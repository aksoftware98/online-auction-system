resource "azurerm_cosmosdb_account" "cosmos-db" {
    name = "oasbiddb"
    resource_group_name = "onlineauctionssytem-rg"
    location = "West US"
    offer_type = "Standard"
    consistency_policy {
      consistency_level = "BoundedStaleness"
      max_interval_in_seconds = 10
      max_staleness_prefix = 200
    }
    geo_location {
        location = "West US"
        failover_priority = 0
    }
}