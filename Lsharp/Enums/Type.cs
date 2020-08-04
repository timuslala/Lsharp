public enum ObjType
{
    GameObject		= (1 << 0),  //0x1
    NeutralCamp		= (1 << 1),  //0x2
    DeadObject		= (1 << 4),  //0x10
    InvalidObject	= (1 << 5),  //0x20
    AIBaseCommon	= (1 << 7),  //0x80
    AttackableUnit	= (1 << 9),  //0x200
    AI				= (1 << 10), //0x400
    Minion			= (1 << 11), //0x800
    Hero			= (1 << 12), //0x1000
    Turret			= (1 << 13), //0x2000
    Unknown0		= (1 << 14), //0x4000
    Missile			= (1 << 15), //0x8000
    Unknown1		= (1 << 16), //0x10000
    Building		= (1 << 17), //0x20000
    Unknown2		= (1 << 18), //0x40000
 
    Particle = AttackableUnit | DeadObject | NeutralCamp

}
