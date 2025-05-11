// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Torrent.Provider;

public class Piece
{
    public Piece(int index)
    {
        this.Index = index;
    }

    public Piece(int index, int size, Sha1Hash hash)
    {
        this.Index = index;
        this.Size = size;
        this.Hash = hash;
    }

    public int Index { get; }

    public int Size { get; }

    public Sha1Hash Hash { get; }
}
