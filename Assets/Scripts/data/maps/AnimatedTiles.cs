﻿using UnityEngine;
using id = TileID;
using AITile = AnimatedIndexedTile;

public static class AnimatedTiles
{
    static Sprite SpriteFromTexture8x8(string path, int x, int y)
    => Sprite.Create(Resources.Load<Texture2D>("Tilesets/" + path),
            new Rect(8 * x, 8 * y, 8, 8), new Vector2(0.5F, 0.5F), 16);

    static Sprite[] SpriteFromWaterAnims(int x, int y)
    => new Sprite[8]
        {
            SpriteFromTexture8x8("Anims/Water/0",x,y),
            SpriteFromTexture8x8("Anims/Water/1",x,y),
            SpriteFromTexture8x8("Anims/Water/2",x,y),
            SpriteFromTexture8x8("Anims/Water/3",x,y),
            SpriteFromTexture8x8("Anims/Water/4",x,y),
            SpriteFromTexture8x8("Anims/Water/5",x,y),
            SpriteFromTexture8x8("Anims/Water/6",x,y),
            SpriteFromTexture8x8("Anims/Water/7",x,y),
        };
    public static Sprite[] water0NWSprites = SpriteFromWaterAnims(0, 14);
    public static Sprite[] water0NESprites = SpriteFromWaterAnims(1, 14);
    public static Sprite[] water0SWSprites = SpriteFromWaterAnims(0, 13);
    public static Sprite[] water0SESprites = SpriteFromWaterAnims(1, 13);

    public static AITile water0NW = AITile.Create(id.water0NW, water0NWSprites, 5F);
    public static AITile water0NE = AITile.Create(id.water0NE, water0NESprites, 5F);
    public static AITile water0SW = AITile.Create(id.water0SW, water0SWSprites, 5F);
    public static AITile water0SE = AITile.Create(id.water0SE, water0SESprites, 5F);
}