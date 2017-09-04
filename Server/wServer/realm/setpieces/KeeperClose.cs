﻿#region

using System;
using db.data;
using terrain;
using wServer.logic.behaviors.Drakes;

#endregion

namespace wServer.realm.setpieces
{
    internal class KeeperClose : ISetPiece
    {
        private static readonly string Wall = "Iinvisible NoWalk";
        private static readonly string Floor = "Black";

        //Ocean Rock


        private readonly Random rand = new Random();

        public int Size
        {
            get { return 50; }
        }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            int radius = 21;

            int[,] t = new int[Size, Size];

            for (int x = 0; x < Size; x++) //Rock
                for (int y = 0; y < Size; y++)
                {
                    double dx = x - (Size / 2.0);
                    double dy = y - (Size / 2.0);
                    double r = Math.Sqrt(dx * dx + dy * dy);
                    if (r >= radius)
                    {
                        t[x, y] = 1;
                    }
                }

            XmlData dat = world.Manager.GameData;
            for (int x = 0; x < Size; x++) //Rendering
                for (int y = 0; y < Size; y++)
                {
                    if (t[x, y] == 1)
                    {
                        WmapTile tile = world.Map[x + pos.X, y + pos.Y].Clone();
                        tile.TileId = dat.IdToTileType[Floor];
                        tile.ObjType = dat.IdToObjectType[Wall];
                        if (tile.ObjId == 0) tile.ObjId = world.GetNextEntityId();
                        world.Map[x + pos.X, y + pos.Y] = tile;
                    }
                }
        }
    }
    internal class KeeperClose1 : ISetPiece
    {
        private static readonly string Wall = "Iinvisible NoWalk";
        private static readonly string Floor = "Black";

        //Ocean Rock


        private readonly Random rand = new Random();

        public int Size
        {
            get { return 50; }
        }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            int radius = 20;

            int[,] t = new int[Size, Size];

            for (int x = 0; x < Size; x++) //Rock
                for (int y = 0; y < Size; y++)
                {
                    double dx = x - (Size / 2.0);
                    double dy = y - (Size / 2.0);
                    double r = Math.Sqrt(dx * dx + dy * dy);
                    if (r >= radius)
                    {
                        t[x, y] = 1;
                    }
                }

            XmlData dat = world.Manager.GameData;
            for (int x = 0; x < Size; x++) //Rendering
                for (int y = 0; y < Size; y++)
                {
                    if (t[x, y] == 1)
                    {
                        WmapTile tile = world.Map[x + pos.X, y + pos.Y].Clone();
                        tile.TileId = dat.IdToTileType[Floor];
                        tile.ObjType = dat.IdToObjectType[Wall];
                        if (tile.ObjId == 0) tile.ObjId = world.GetNextEntityId();
                        world.Map[x + pos.X, y + pos.Y] = tile;
                    }
                }
        }
    }
    internal class KeeperClose2 : ISetPiece
    {
        private static readonly string Wall = "Iinvisible NoWalk";
        private static readonly string Floor = "Black";

        //Ocean Rock


        private readonly Random rand = new Random();

        public int Size
        {
            get { return 50; }
        }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            int radius = 19;

            int[,] t = new int[Size, Size];

            for (int x = 0; x < Size; x++) //Rock
                for (int y = 0; y < Size; y++)
                {
                    double dx = x - (Size / 2.0);
                    double dy = y - (Size / 2.0);
                    double r = Math.Sqrt(dx * dx + dy * dy);
                    if (r >= radius)
                    {
                        t[x, y] = 1;
                    }
                }

            XmlData dat = world.Manager.GameData;
            for (int x = 0; x < Size; x++) //Rendering
                for (int y = 0; y < Size; y++)
                {
                    if (t[x, y] == 1)
                    {
                        WmapTile tile = world.Map[x + pos.X, y + pos.Y].Clone();
                        tile.TileId = dat.IdToTileType[Floor];
                        tile.ObjType = dat.IdToObjectType[Wall];
                        if (tile.ObjId == 0) tile.ObjId = world.GetNextEntityId();
                        world.Map[x + pos.X, y + pos.Y] = tile;
                    }
                }
        }
    }
    internal class KeeperClose3 : ISetPiece
    {
        private static readonly string Wall = "Iinvisible NoWalk";
        private static readonly string Floor = "Black";

        //Ocean Rock


        private readonly Random rand = new Random();

        public int Size
        {
            get { return 50; }
        }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            int radius = 18;

            int[,] t = new int[Size, Size];

            for (int x = 0; x < Size; x++) //Rock
                for (int y = 0; y < Size; y++)
                {
                    double dx = x - (Size / 2.0);
                    double dy = y - (Size / 2.0);
                    double r = Math.Sqrt(dx * dx + dy * dy);
                    if (r >= radius)
                    {
                        t[x, y] = 1;
                    }
                }

            XmlData dat = world.Manager.GameData;
            for (int x = 0; x < Size; x++) //Rendering
                for (int y = 0; y < Size; y++)
                {
                    if (t[x, y] == 1)
                    {
                        WmapTile tile = world.Map[x + pos.X, y + pos.Y].Clone();
                        tile.TileId = dat.IdToTileType[Floor];
                        tile.ObjType = dat.IdToObjectType[Wall];
                        if (tile.ObjId == 0) tile.ObjId = world.GetNextEntityId();
                        world.Map[x + pos.X, y + pos.Y] = tile;
                    }
                }
        }
    }
}