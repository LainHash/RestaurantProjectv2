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
        public Result DeductInventoryForOrder(
            long branchId,
            IEnumerable<(Product Product, int Quantity)> items,
            CancellationToken cancellationToken = default)
        {
            var productStockDemands = new Dictionary<long, (ProductStock Stock, string ProductName, int RequiredQuantity)>();

            var ingredientDemands = new Dictionary<long, (Ingredient Ingredient, decimal RequiredAmount)>();

            foreach (var (product, quantity) in items)
            {
                if (quantity <= 0) continue;

                if (product.InventoryType == InventoryType.StockTracked)
                {
                    var stock = product.ProductStocks.FirstOrDefault(s => s.BranchId == branchId);
                    if (stock is null)
                    {
                        return Result.Fail(
                            $"Sản phẩm '{product.Name}' chưa được thiết lập dữ liệu tồn kho tại chi nhánh này.",
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
                            $"Món '{product.Name}' chưa được cấu hình công thức chế biến (Recipe).",
                            HttpStatusCode.BadRequest);
                    }

                    foreach (var ri in recipe.RecipeIngredients)
                    {
                        if (ri.Ingredient is null)
                        {
                            return Result.Fail(
                                $"Không tìm thấy thông tin nguyên liệu trong công thức của món '{product.Name}'.",
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
                            ingredientDemands[ri.IngredientId] = (ri.Ingredient, existing.RequiredAmount + requiredAmount);
                        }
                        else
                        {
                            ingredientDemands[ri.IngredientId] = (ri.Ingredient, requiredAmount);
                        }
                    }
                }
            }

            foreach (var (stock, productName, requiredQty) in productStockDemands.Values)
            {
                if (stock.QuantityOnHand < requiredQty)
                {
                    return Result.Fail(
                        $"Không đủ tồn kho cho sản phẩm '{productName}'. Tồn hiện tại: {stock.QuantityOnHand}, Yêu cầu: {requiredQty}.",
                        HttpStatusCode.BadRequest);
                }

                stock.UpdateQuantity(-requiredQty);
            }

            foreach (var (ingredient, requiredAmount) in ingredientDemands.Values)
            {
                var ingredientStock = ingredient.IngredientStocks.FirstOrDefault(s => s.BranchId == branchId);
                var availableOnHand = ingredientStock?.QuantityOnHand ?? 0m;
                var unitSymbol = ingredient.BaseUnit?.Symbol ?? string.Empty;

                if (ingredientStock is null || availableOnHand < requiredAmount)
                {
                    return Result.Fail(
                        $"Không đủ nguyên liệu '{ingredient.Name}'. Tồn hiện tại: {availableOnHand} {unitSymbol}, Cần: {requiredAmount} {unitSymbol}.",
                        HttpStatusCode.BadRequest);
                }

                ingredientStock.UpdateQuantity(-requiredAmount);
            }

            return Result.Succeed("Cập nhật tồn kho thành công.");
        }
    }
}
