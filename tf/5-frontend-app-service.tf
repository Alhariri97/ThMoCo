resource "azurerm_windows_web_app" "frontend_app" {
    location = azurerm_resource_group.the_resource_group.location
    resource_group_name = azurerm_resource_group.the_resource_group.name
    name = "${var.the_prefix}-frontend-app"
    service_plan_id = azurerm_service_plan.frontend_plan.id
    https_only = "true"
    tags = var.resource_tags

    site_config {
        always_on = "false"
        default_documents = ["index.html"] # For static content
    }
    app_settings = {
        "WEBSITE_RUN_FROM_PACKAGE" = "1" # Optional for single-package deployment
    }
}