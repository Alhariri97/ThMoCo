#################################################################
#   Variables
#################################################################


#Environment Name
the_environment_name = "devah"

#Tags
resource_tags = {
                Environment = "Dev-AH"
                Department = "Development"
                Owner = "Terraform"
                "Application ID" = "ThAmCo"
                Application = "Terraform"
                Criticality = "Tier 3"
}

# Resource Group
the_resource_group = "thamco-devah-rg"
the_location = "uksouth"
the_prefix = "thamcodevah"

# App Services for API
allowed_cors_origin = "*"

# App Services Plan
app_service_plan_sku_size = "F1"

# Azure Api Management
api_mgmt_publisher_name = "Abdul hariri"
api_mgmt_publisher_email = "19abdul97@gmail.com"
api_mgmt_sku = "Consumption"

# Storage Accounts
storage_account_access_tier = "Hot"
storage_account_account_replication_tier = "LRS"
storage_account_account_sku = "Standard"

#Sql Server
sql-server-admin-username = "admin.sql"
sql-server-admin-password = "ampc7Vu8&xY3J7DX20iAM#hcu80"
