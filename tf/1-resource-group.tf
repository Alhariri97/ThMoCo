resource "azurerm_resource_group" "the_resource_group" {
    name=   var.the_resource_group
    location= var.the_location
    tags = var.resource_tags
}