resource "azurerm_service_plan" "app" {
    name=   "${var.the_prefix}-app-srv-plan"
    location = azurerm_resource_group.the_resource_group.location
    resource_group_name = azurerm_resource_group.the_resource_group.name
    os_type = "Windows"
    sku_name =  var.app_service_plan_sku_size
    tags = var.resource_tags
}