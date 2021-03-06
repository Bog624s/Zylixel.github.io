﻿#region

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using wServer.networking.svrPackets;
using wServer.realm.entities.player;
using System.Linq;

#endregion

namespace wServer.realm.entities.merchant
{
    public class Merchants : SellableObject
    {
        private const int BuyNoFame = 6;
        private const int MerchantSize = 100;

        private int _tickcount;
        private int _accId;
        private int _itemChange;

        public static Random Random { get; private set; }
          
        public Merchants(RealmManager manager, ushort objType, int itemChangeLocal = -1, World owner = null)
            : base(manager, objType)
        {
            Size = MerchantSize;
            if (owner != null)
                Owner = owner;

            _itemChange = itemChangeLocal;

            if (Random == null) Random = new Random();
            if (owner != null) ResolveMType();
        }
        
        public int MType { get; set; }
        public int MTime { get; set; }

        protected override void ExportStats(IDictionary<StatsType, object> stats)
        {
            stats[StatsType.MerchantMerchandiseType] = MType;
            stats[StatsType.SellablePrice] = Price;
            stats[StatsType.SellablePriceCurrency] = Currency;

            base.ExportStats(stats);
        }

        public override void Init(World owner)
        {
            base.Init(owner);
            ResolveMType();
            UpdateCount++;
        }

        protected override bool TryDeduct(Player player)
        {
            return player.Client.Account.Stats.Fame >= Price;
        }

        public override void Buy(Player player)
        {
            Manager.Database.DoActionAsync(db =>
            {
                if (ObjectType == 0x01ca) //Merchant
                {
                    if (TryDeduct(player))
                    {
                        for (var i = 0; i < player.Inventory.Length; i++)
                        {
                            try
                            {
                                XElement ist;
                                Manager.GameData.ObjectTypeToElement.TryGetValue((ushort)MType, out ist);
                                if (player.Inventory[i] == null &&
                                    (player.SlotTypes[i] == 10 ||
                                     player.SlotTypes[i] == Convert.ToInt16(ist.Element("SlotType").Value)))
                                {
                                    player.Inventory[i] = Manager.GameData.Items[(ushort)MType];
                                    
                                    player.CurrentFame =
                                        player.Client.Account.Stats.Fame =
                                            db.UpdateFame(player.Client.Account, -Price);

                                    {
                                        _accId = db.GetMarketCharId(MType, Price);
                                    }
                                    {
                                        if (logic.CheckConfig.IsDebugOn())
                                            Console.WriteLine("Attempted to give Player " + _accId + ", " + Price + " fame");
                                        MySqlCommand cmd = db.CreateQuery();
                                        cmd.CommandText = "UPDATE stats SET fame = fame + @Price WHERE accId=@accId";
                                        cmd.Parameters.AddWithValue("@accId", _accId);
                                        cmd.Parameters.AddWithValue("@Price", Price);
                                        cmd.ExecuteNonQuery();
                                    }
                                    {
                                        if (logic.CheckConfig.IsDebugOn())
                                            Console.WriteLine("Attemping to delete item from database: " + MType + " | " + Price);
                                        MySqlCommand cmd = db.CreateQuery();
                                        cmd.CommandText = "DELETE FROM market WHERE itemid=@itemid AND fame=@fame AND playerid=@id";
                                        cmd.Parameters.AddWithValue("@itemid", MType);
                                        cmd.Parameters.AddWithValue("@fame", Price);
                                        cmd.Parameters.AddWithValue("@id", _accId);
                                        cmd.ExecuteNonQuery();
                                    }
                                    player.Client.SendPacket(new BuyResultPacket
                                    {
                                        Result = 0,
                                        Message = "{\"key\":\"server.buy_success\"}"
                                    });
                                    player.Client.Save();
                                    player.UpdateCount++;
                                    UpdateCount++;

                                    Merchant.updatePrice(MType, Manager);

                                    return;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        player.Client.SendPacket(new BuyResultPacket
                        {
                            Result = 0,
                            Message = "{\"key\":\"server.inventory_full\"}"
                        });
                    }
                    else
                    {
                        switch (Currency)
                        {
                            case CurrencyType.Fame:
                                player.Client.SendPacket(new BuyResultPacket
                                {
                                    Result = BuyNoFame,
                                    Message = "{\"key\":\"server.not_enough_fame\"}"
                                });
                                break;
                        }
                    }
                }
            });
        }

        public override void Tick(RealmTime time)
        {
            try
            {
                if (MType == -1)
                {
                    Owner?.LeaveWorld(this);
                }
                else if (MTime == -1 && Owner != null || !MerchantLists.ZyList.Contains(MType))
                {
                    Recreate(this, false);
                    UpdateCount++;
                }
                else if (Price != MerchantLists.price[MType])
                {
                    Recreate(this, true);
                    UpdateCount++;
                }
                
                _tickcount++;
                if (_tickcount % (Manager?.TPS * 60) == 0) //once per minute after spawning
                {
                    MTime--;
                    UpdateCount++;
                } 

                

                base.Tick(time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Recreate(Merchants x, bool keepMType)
        {
            Merchants mrc;
            if (keepMType)
                mrc = new Merchants(Manager, x.ObjectType, MType, x.Owner);
            else
                mrc = new Merchants(Manager, x.ObjectType);

            mrc.Move(x.X, x.Y);
            var w = Owner;
            Owner.LeaveWorld(this);
            w.Timers.Add(new WorldTimer(1, (world, time) => w.EnterWorld(mrc)));
        }

        public void ResolveMType()
        {
            MType = -1;
            var list = MerchantLists.ZyList;
            
            list.Shuffle();
            foreach (var t1 in list)
            {
                if (_itemChange != -1)
                {
                    MType = _itemChange;
                    if (logic.CheckConfig.IsDebugOn())
                        Console.WriteLine("Refreshing Merchant " + MType);
                }
                else
                {
                    {
                        MType = t1;
                        if (logic.CheckConfig.IsDebugOn())
                            Console.WriteLine("Randomizing Merchant to be " + t1);
                    }
                }

                MTime = Random.Next(2, 5);
                
                Price = MerchantLists.price[MType];
                Currency = CurrencyType.Fame;
                
                UpdateCount++;
                return;
            }
        }
    }
}