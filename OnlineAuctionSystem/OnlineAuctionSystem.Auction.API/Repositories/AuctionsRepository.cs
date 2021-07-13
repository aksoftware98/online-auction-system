using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuctionSystem.Auction.API.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace OnlineAuctionSystem.Auction.API.Repositories
{

    public class AuctionsRepository : IAuctionsRepository
    {

        private readonly MySqlConnection _connection;

        public AuctionsRepository(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public async Task<Models.Auction> AddAsync(Models.Auction auction)
        {
            string query = "INSERT INTO `oasdb`.`auctions`" +
                            "(`name`,`description`,`startingPrice`,`auctionDate`,`status`,`image`,`activeInHours`,`bidPrice`,`userId`,`isActive`,`userName`,`isPaymentMade`,`bidUser`,`bidId`) VALUES " +
                            "(@name,@description,@startingPrice,@auctionDate,@status,@image,@activeInHours," +
                            "@bidPrice,@userId,@isActive,@username,@isPaymentMade,@bidUser,@bidId); ";

            var command = new MySqlCommand(query, _connection);
            // Set the parameters
            command.Parameters.AddWithValue("@name", auction.Name);
            command.Parameters.AddWithValue("@description", auction.Description);
            command.Parameters.AddWithValue("@startingPrice", auction.StartPrice);
            command.Parameters.AddWithValue("@auctionDate", auction.AuctionDate);
            command.Parameters.AddWithValue("@status", auction.Status);
            command.Parameters.AddWithValue("@image", auction.Image);
            command.Parameters.AddWithValue("@activeInHours", auction.ActiveInHours);
            command.Parameters.AddWithValue("@bidPrice", auction.BidPrice);
            command.Parameters.AddWithValue("@userId", auction.UserId);
            command.Parameters.AddWithValue("@isActive", auction.IsActive);
            command.Parameters.AddWithValue("@username", auction.Username);
            command.Parameters.AddWithValue("@isPaymentMade", auction.IsPaymentMade);
            command.Parameters.AddWithValue("@bidUser", auction.BidUserId);
            command.Parameters.AddWithValue("@bidId", auction.BidId);

            _connection.Open();
            var result = await command.ExecuteNonQueryAsync();
            _connection.Close();
            auction.Id = (int)result;
            return auction;
        }

        public async Task<IEnumerable<Models.Auction>> GetAllAsync()
        {
            string query = "SELECT * FROM auctions WHERE isActive = 1 ORDER BY auctionDate DESC";
            var command = new MySqlCommand(query, _connection);
            return await GetAuctions(command); 
        }

        public async Task<IEnumerable<Models.Auction>> GetAllByUserIdAsync(string userId)
        {
            string query = "SELECT * FROM auctions WHERE userId = @userId AND isActive = 1 ORDER BY auctionDate DESC";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@userId", userId);
            return await GetAuctions(command);
        }

        public async Task<IEnumerable<Models.Auction>> GetAllByBidUserIdAsync(string bidUserId)
        {
            string query = "SELECT * FROM auctions WHERE bidUser = @bidUser AND isActive = 1 ORDER BY auctionDate DESC";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@bidUser", bidUserId);
            return await GetAuctions(command);
        }

        /// <summary>
        /// Retrieve a list of auctions based on a specific query and return them as a plain POCOs
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Models.Auction>> GetAuctions(MySqlCommand command)
        {
            _connection.Open();
            var dataReader = await command.ExecuteReaderAsync();

            var auctions = new List<Models.Auction>();
            while (dataReader.Read())
            {
                var auction = new Models.Auction();

                auction.Id = (int)dataReader["idAuction"];
                auction.Name = dataReader["name"].ToString();
                auction.Description = dataReader["description"]?.ToString();
                auction.ActiveInHours = (int)dataReader["activeInHours"];
                auction.AuctionDate = (DateTime)dataReader["auctionDate"];
                auction.BidId = dataReader["bidId"].ToString();
                auction.BidPrice = (decimal?)(dataReader["bidPrice"] ?? 0);
                auction.BidUserId = dataReader["bidUser"]?.ToString();
                auction.Image = dataReader["image"].ToString();
                auction.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                auction.IsPaymentMade = Convert.ToBoolean((sbyte)dataReader["isPaymentMade"]);
                auction.StartPrice = (decimal)dataReader["startingPrice"];
                auction.UserId = dataReader["userId"].ToString();
                auction.Status = (int)dataReader["status"];
                auction.Username = dataReader["username"].ToString();
                
                auctions.Add(auction);
            }

            _connection.Close(); 
            return auctions;
        }

        public async Task<Models.Auction> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM auctions WHERE idAuction = @id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            _connection.Open();
            var result = await command.ExecuteReaderAsync();
            var firstResult = result.Read();
            if (!firstResult)
            {
                _connection.Close();
                return null;
            }
            var auction = new Models.Auction()
            {
                Id = (int)result["idAuction"],
                Name = result["name"].ToString(),
                Description = result["description"]?.ToString(),
                ActiveInHours = (int)result["activeInHours"],
                AuctionDate = (DateTime)result["auctionDate"],
                BidId = result["bidId"].ToString(),
                BidPrice = (decimal)result["bidPrice"],
                BidUserId = result["bidUserId"]?.ToString(),
                Image = result["image"].ToString(),
                IsActive = Convert.ToBoolean(result["isActive"]),
                IsPaymentMade = Convert.ToBoolean(result["isPaymentMade"]),
                StartPrice = (decimal)result["startPrice"],
                Status = (int)result["status"],
                Username = result["username"].ToString(),
                UserId = result["userId"]?.ToString(),
            };
            _connection.Close();
            return auction;
        }

        public async Task<Models.Auction> UpdateAsync(Models.Auction auction)
        {
            string query = "UPDATE  `oasdb`.`auctions` SET `name` = @name,`description` = @description,`startingPrice` = @startingPrice,`auctionDate` = @auctionDate,`status` = @status,`image` = @image,`activeInHours` = @activeInHours,`bidPrice` = @bidPrice,`userId` = @userId,`isActive` = @isActive,`userName` = @username,`isPaymentMade` = @isPaymentMade,`bidUser` = @bidUser,`bidId` = @bidId WHERE `idAuction` = @id";

            var command = new MySqlCommand(query, _connection);
            // Set the parameters
            command.Parameters.AddWithValue("@name", auction.Name);
            command.Parameters.AddWithValue("@description", auction.Description);
            command.Parameters.AddWithValue("@startingPrice", auction.StartPrice);
            command.Parameters.AddWithValue("@auctionDate", auction.AuctionDate);
            command.Parameters.AddWithValue("@status", auction.Status);
            command.Parameters.AddWithValue("@image", auction.Image);
            command.Parameters.AddWithValue("@activeInHours", auction.ActiveInHours);
            command.Parameters.AddWithValue("@bidPrice", auction.BidPrice);
            command.Parameters.AddWithValue("@userId", auction.BidUserId);
            command.Parameters.AddWithValue("@isActive", auction.IsActive);
            command.Parameters.AddWithValue("@username", auction.Username);
            command.Parameters.AddWithValue("@isPaymentMade", auction.IsPaymentMade);
            command.Parameters.AddWithValue("@bidUser", auction.BidUserId);
            command.Parameters.AddWithValue("@bidId", auction.BidId);
            command.Parameters.AddWithValue("@id", auction.Id);

            _connection.Open();
            var result = await command.ExecuteScalarAsync();
            _connection.Close();
            auction.Id = (int)result;
            return auction;
        }
    }

}
