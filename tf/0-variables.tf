#################################################################
#   Variables
#################################################################

# Provider info
variable subscription_id {}
variable tenant_id {}
variable client_id {}
variable client_secret {}

# Generic info
variable the_location {}

variable the_resource_group {}

variable the_environment_name {}

variable the_prefix {}

variable resource_tags {}

# App Services for UI and API
variable app_service_plan_sku_tier {
    description = "The App Service Plan Size"
    default = "Free"
}

variable app_service_plan_sku_size {
    description = "The App Service Plan Size"
    default = "F1"
}

variable allowed_cors_origin {}

# Azure API Management
variable api_mgmt_publisher_name {
    description = "The name of the publisher of the Api"
    default = "Abdul hariri"
}
variable api_mgmt_publisher_email {
    description = "The support email of the publisher of the Api"
    default = "19abdul97@gmail.com"
}

variable api_mgmt_sku {
    description = "The product sku tier to use for Azure API Management"
    default = "Consumption"
}

# Storage Accounts
variable storage_account_access_tier {}
variable storage_account_account_replication_tier {}
variable storage_account_account_sku {
    description = "The Storage Account type, Premium and Standard."
    default = "Standard"
}

# Service Bus
variable service_bus_sku {
    default = "Standard"
    description = "The version of Service Bus to run"
}

# SQL Server
variable sql-server-admin-username {
    default = "admin.sql"
    description = "The SQL Server Admin Username"
}

variable sql-server-admin-password {
    description = "The SQL Server Admin Password"
}

variable sql-server-database-edition {
    description = "The performance of the database"
    default = "Basic"
}
