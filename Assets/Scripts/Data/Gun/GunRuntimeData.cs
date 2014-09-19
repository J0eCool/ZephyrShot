using UnityEngine;

public class GunRuntimeData {
    public GunData data;
    public Timer shotTimer = new Timer();
    public Timer timeFiring = new Timer();
    public int shotsFired = 0;
    public int bulletsFired = 0;

    public int powerupLevel = 0;
    public int bulletLevel = 0;

    public GunRuntimeData(GunData data) {
        this.data = data;
    }
}
