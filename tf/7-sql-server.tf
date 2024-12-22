resource "azurerm_mssql_server" "sql" {
    location = azurerm_resource_group.the_resource_group.location
    resource_group_name = azurerm_resource_group.the_resource_group.name
    name = "${var.the_prefix}-sql-server"
    version = "12.0"
    administrator_login = var.sql-server-admin-username
    administrator_login_password = var.sql-server-admin-password
    minimum_tls_version          = "1.2"
    tags = var.resource_tags
}