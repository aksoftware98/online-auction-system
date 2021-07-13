provider "azurerm" {
    features{}
}

resource "azurerm_resource_group" "rg" {
    name = "onlineauctionssytem-rg"
    location = "West US"
}

resource "azurerm_mysql_server" "resources-values" {
    name = "mysqloas"
    location = "West US"
    resource_group_name = "onlineauctionssytem-rg"
    administrator_login = "ak_admin"
    administrator_login_password = "zd3ujp%D@KbvS2#A2U"
    sku_name = "B_Gen5_2"
    storage_mb = 5120
    version = "5.7"
    auto_grow_enabled = true
    backup_retention_days = 7
    ssl_enforcement_enabled = false
}