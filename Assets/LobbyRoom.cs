using Photon.Realtime;

public class LobbyRoom
{
    private readonly RoomInfo roomInfo;

    public string RoomName { get; set; }
    public int playersCount { get; set; }

    public RoomInfo RoomInfo => roomInfo;

    public LobbyRoom(RoomInfo roomInfo)
    {
        this.roomInfo = roomInfo;
    }


}