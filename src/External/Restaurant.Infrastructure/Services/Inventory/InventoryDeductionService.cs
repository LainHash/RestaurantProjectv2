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
        public Task<Result> DeductInventoryForOrderAsync(
            int branchId,
            IEnumerable<(Product Product, int Quantity)> items,
            CancellationToken cancellationToken = default)
        {
            // 1. Gom nhóm nhu cầu tồn kho cho sản phẩm đóng gói/bán sẵn (StockTracked)
            var productStockDemands = new Dictionary<int, (ProductStock Stock, string ProductName, int RequiredQuantity)>();

            // 2. Gom nhóm nhu cầu tồn kho cho nguyên liệu (MadeToOrder)
            var ingredientDemands = new Dictionary<int, (Ingredient Ingredient, decimal RequiredAmount)>();

            foreach (var (product, quantity) in items)
            {
                if (quantity <= 0) continue;

                if (product.InventoryType == InventoryType.StockTracked)
                {
                    var stock = product.ProductStocks.FirstOrDefault(s => s.BranchId == branchId);
                    if (stock is null)
                    {
                        return Task.FromResult(Result.Fail(
                            $"Sản phẩm '{product.Name}' chưa được thiết lập dữ liệu tồn kho tại chi nhánh này.",
                            HttpStatusCode.BadRequest));
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
                        return Task.FromResult(Result.Fail(
                            $"Món '{product.Name}' chưa được cấu hình công thức chế biến (Recipe).",
                            HttpStatusCode.BadRequest));
                    }

                    foreach (var ri in recipe.RecipeIngredients)
                    {
                        if (ri.Ingredient is null)
                        {
                            return Task.FromResult(Result.Fail(
                                $"Không tìm thấy thông tin nguyên liệu trong công thức của món '{product.Name}'.",
                                HttpStatusCode.BadRequest));
                        }

                        // Tính hệ số quy đổi đơn vị về BaseUnit của nguyên liệu
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

            // 3. Kiểm tra tính khả dụng & trừ tồn kho thành phẩm (StockTracked)
            foreach (var (stock, productName, requiredQty) in productStockDemands.Values)
            {
                if (stock.QuantityOnHand < requiredQty)
                {
                    return Task.FromResult(Result.Fail(
                        $"Không đủ tồn kho cho sản phẩm '{productName}'. Tồn hiện tại: {stock.QuantityOnHand}, Yêu cầu: {requiredQty}.",
                        HttpStatusCode.BadRequest));
                }

                stock.UpdateQuantity(-requiredQty);
            }

            // 4. Kiểm tra tính khả dụng & trừ tồn kho nguyên liệu (MadeToOrder)
            foreach (var (ingredient, requiredAmount) in ingredientDemands.Values)
            {
                var ingredientStock = ingredient.IngredientStocks.FirstOrDefault(s => s.BranchId == branchId);
                var availableOnHand = ingredientStock?.QuantityOnHand ?? 0m;
                var unitSymbol = ingredient.BaseUnit?.Symbol ?? string.Empty;

                if (ingredientStock is null || availableOnHand < requiredAmount)
                {
                    return Task.FromResult(Result.Fail(
                        $"Không đủ nguyên liệu '{ingredient.Name}'. Tồn hiện tại: {availableOnHand} {unitSymbol}, Cần: {requiredAmount} {unitSymbol}.",
                        HttpStatusCode.BadRequest));
                }

                ingredientStock.UpdateQuantity(-requiredAmount);
            }

            return Task.FromResult(Result.Succeed("Cập nhật tồn kho thành công."));
        }
    }
}
