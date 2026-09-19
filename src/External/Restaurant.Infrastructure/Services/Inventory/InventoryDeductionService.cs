using Restaurant.Application.Services.Inventory;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Results;
using System.Net;

namespace Restaurant.Infrastructure.Services.Inventory
{
    internal class InventoryDeductionService : IInventoryDeductionService
    {
        public Result ReserveInventoryForOrder(
            long branchId,
            IEnumerable<(Product Product, int Quantity)> items,
            CancellationToken cancellationToken = default)
        {
            // Phase 1: aggregate total demand per stock/ingredient to avoid
            // partial-reserve state when the same item appears multiple times.
            var productStockDemands = new Dictionary<long, (ProductStock Stock, string ProductName, decimal RequiredQuantity)>();
            var ingredientDemands = new Dictionary<long, (IngredientStock Stock, Ingredient Ingredient, decimal RequiredAmount)>();

            foreach (var (product, quantity) in items)
            {
                if (quantity <= 0) continue;

                if (product.InventoryType == InventoryType.StockTracked)
                {
                    var stock = product.ProductStocks.FirstOrDefault(s => s.BranchId == branchId);
                    if (stock is null)
                    {
                        return Result.Fail(
                            $"Product '{product.Name}' does not have stock data configured for this branch.",
                            HttpStatusCode.BadRequest);
                    }

                    if (productStockDemands.TryGetValue(product.Id, out var existing))
                    {
                        productStockDemands[product.Id] = (stock, product.Name, existing.RequiredQuantity + quantity);
                    }
                    else
                    {
                        productStockDemands[product.Id] = (stock, product.Name, quantity);
                    }
                }
                else if (product.InventoryType == InventoryType.MadeToOrder)
                {
                    var recipe = product.Recipes.FirstOrDefault();
                    if (recipe is null || !recipe.RecipeIngredients.Any())
                    {
                        return Result.Fail(
                            $"Product '{product.Name}' does not have a recipe configured.",
                            HttpStatusCode.BadRequest);
                    }

                    foreach (var ri in recipe.RecipeIngredients)
                    {
                        if (ri.Ingredient is null)
                        {
                            return Result.Fail(
                                $"A recipe ingredient for product '{product.Name}' could not be resolved.",
                                HttpStatusCode.BadRequest);
                        }

                        var ingredientStock = ri.Ingredient.IngredientStocks.FirstOrDefault(s => s.BranchId == branchId);
                        if (ingredientStock is null)
                        {
                            return Result.Fail(
                                $"Ingredient '{ri.Ingredient.Name}' does not have stock data configured for this branch.",
                                HttpStatusCode.BadRequest);
                        }

                        decimal conversionFactor = 1m;
                        if (ri.UnitId != ri.Ingredient.BaseUnitId &&
                            ri.Ingredient.BaseUnit is not null &&
                            ri.Ingredient.BaseUnit.ConversionRate > 0)
                        {
                            var recipeUnitRate = ri.Unit?.ConversionRate ?? 1m;
                            conversionFactor = recipeUnitRate / ri.Ingredient.BaseUnit.ConversionRate;
                        }

                        decimal requiredAmount = quantity * ri.Quantity * conversionFactor;

                        if (ingredientDemands.TryGetValue(ri.IngredientId, out var existing))
                        {
                            ingredientDemands[ri.IngredientId] = (ingredientStock, ri.Ingredient, existing.RequiredAmount + requiredAmount);
                        }
                        else
                        {
                            ingredientDemands[ri.IngredientId] = (ingredientStock, ri.Ingredient, requiredAmount);
                        }
                    }
                }
            }

            // Phase 2: validate available quantity (QuantityOnHand - QuantityReserved) for all items
            // before mutating anything, to avoid partial-reserve on failure.
            foreach (var (stock, productName, requiredQty) in productStockDemands.Values)
            {
                if (stock.AvailableQuantity < requiredQty)
                {
                    return Result.Fail(
                        $"Insufficient stock for product '{productName}'. " +
                        $"Available: {stock.AvailableQuantity} (On hand: {stock.QuantityOnHand}, Reserved: {stock.QuantityReserved}), " +
                        $"Required: {requiredQty}.",
                        HttpStatusCode.BadRequest);
                }
            }

            foreach (var (ingredientStock, ingredient, requiredAmount) in ingredientDemands.Values)
            {
                var unitSymbol = ingredient.BaseUnit?.Symbol ?? string.Empty;
                if (ingredientStock.AvailableQuantity < requiredAmount)
                {
                    return Result.Fail(
                        $"Insufficient stock for ingredient '{ingredient.Name}'. " +
                        $"Available: {ingredientStock.AvailableQuantity} {unitSymbol} " +
                        $"(On hand: {ingredientStock.QuantityOnHand}, Reserved: {ingredientStock.QuantityReserved}), " +
                        $"Required: {requiredAmount} {unitSymbol}.",
                        HttpStatusCode.BadRequest);
                }
            }

            // Phase 3: all checks passed — reserve stock (does NOT deduct QuantityOnHand).
            foreach (var (stock, _, requiredQty) in productStockDemands.Values)
            {
                stock.Reserve(requiredQty);
            }

            foreach (var (ingredientStock, _, requiredAmount) in ingredientDemands.Values)
            {
                ingredientStock.Reserve(requiredAmount);
            }

            return Result.Succeed("Inventory reserved successfully.");
        }

        public Result ReleaseReservedInventoryForOrder(
            long branchId,
            IEnumerable<(Product Product, int Quantity)> items,
            CancellationToken cancellationToken = default)
        {
            var productStockDemands = new Dictionary<long, (ProductStock Stock, decimal RequiredQuantity)>();
            var ingredientDemands = new Dictionary<long, (IngredientStock Stock, decimal RequiredAmount)>();

            foreach (var (product, quantity) in items)
            {
                if (quantity <= 0) continue;

                if (product.InventoryType == InventoryType.StockTracked)
                {
                    var stock = product.ProductStocks.FirstOrDefault(s => s.BranchId == branchId);
                    if (stock is null) continue;

                    if (productStockDemands.TryGetValue(product.Id, out var existing))
                    {
                        productStockDemands[product.Id] = (stock, existing.RequiredQuantity + quantity);
                    }
                    else
                    {
                        productStockDemands[product.Id] = (stock, quantity);
                    }
                }
                else if (product.InventoryType == InventoryType.MadeToOrder)
                {
                    var recipe = product.Recipes.FirstOrDefault();
                    if (recipe is null || !recipe.RecipeIngredients.Any()) continue;

                    foreach (var ri in recipe.RecipeIngredients)
                    {
                        if (ri.Ingredient is null) continue;

                        var ingredientStock = ri.Ingredient.IngredientStocks.FirstOrDefault(s => s.BranchId == branchId);
                        if (ingredientStock is null) continue;

                        decimal conversionFactor = 1m;
                        if (ri.UnitId != ri.Ingredient.BaseUnitId &&
                            ri.Ingredient.BaseUnit is not null &&
                            ri.Ingredient.BaseUnit.ConversionRate > 0)
                        {
                            var recipeUnitRate = ri.Unit?.ConversionRate ?? 1m;
                            conversionFactor = recipeUnitRate / ri.Ingredient.BaseUnit.ConversionRate;
                        }

                        decimal requiredAmount = quantity * ri.Quantity * conversionFactor;

                        if (ingredientDemands.TryGetValue(ri.IngredientId, out var existing))
                        {
                            ingredientDemands[ri.IngredientId] = (ingredientStock, existing.RequiredAmount + requiredAmount);
                        }
                        else
                        {
                            ingredientDemands[ri.IngredientId] = (ingredientStock, requiredAmount);
                        }
                    }
                }
            }

            foreach (var (stock, requiredQty) in productStockDemands.Values)
            {
                stock.Release(requiredQty);
            }

            foreach (var (ingredientStock, requiredAmount) in ingredientDemands.Values)
            {
                ingredientStock.Release(requiredAmount);
            }

            return Result.Succeed("Reserved inventory released successfully.");
        }
    }
}
