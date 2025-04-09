using BencodeNET.Objects;
using BencodeNET.Parsing;

namespace H.Extensions.Torrent;

internal class Class1
{


    public void Method()
    {
        // Parse torrent by specifying the file path
        var parser = new BencodeParser(); // Default encoding is Encoding.UTF8, but you can specify another if you need to
        BencodeNET.Torrents.Torrent torrent = parser.Parse<BencodeNET.Torrents.Torrent>("C:\\ubuntu.torrent");

        //// Or parse a stream
        //var torrent = parser.Parse<BencodeNET.Torrents.Torrent>(stream);

        // Calculate the info hash
        string infoHash = torrent.GetInfoHash();
        // "B415C913643E5FF49FE37D304BBB5E6E11AD5101"

        // or as bytes instead of a string
        byte[] infoHashBytes = torrent.GetInfoHashBytes();

        // Get Magnet link
        string magnetLink = torrent.GetMagnetLink();
        // magnet:?xt=urn:btih:1CA512A4822EDC7C1B1CE354D7B8D2F84EE11C32&dn=ubuntu-14.10-desktop-amd64.iso&tr=http://torrent.ubuntu.com:6969/announce&tr=http://ipv6.torrent.ubuntu.com:6969/announce

        // Convert Torrent to its BDictionary representation
        BDictionary bdictinoary = torrent.ToBDictionary();
    }

}
