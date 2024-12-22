resource "azurerm_mssql_database" "sql_database" {
    name                         = "${var.the_prefix}-db"
    server_id         = azurerm_mssql_server.sql.id
    create_mode         = "Default"
    sku_name             = var.sql-server-database-edition
    max_size_gb         = 2
    tags = var.resource_tags
}