resource "azurerm_windows_web_app" "app" {
    location = azurerm_resource_group.the_resource_group.location
    resource_group_name = azurerm_resource_group.the_resource_group.name
    name = "${var.the_prefix}-app"
    service_plan_id = azurerm_service_plan.app.id
    https_only = "true"
    tags = var.resource_tags

    site_config {
        always_on = "false"
    }
}