namespace PromisedEigong;

public static class PromisedEigongModGlobalSettings
{
    public static class EigongLocs
    {
        public const string EIGONG_TITLE_LOC = "Characters/NameTag_YiKong"; 
    }

    public static class EigongRefs
    {
        public const string SCENE_NORMAL_ENDING_EIGONG = "A11_S0_Boss_YiGung_回蓬萊";
        public const string SCENE_TRUE_ENDING_EIGONG = "A11_S0_Boss_YiGung";
        public const string BIND_MONSTER_EIGONG_NAME = "Boss_Yi Gung";
    }
    
    public static class EigongAttacks
    {
        public const string STATES_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/FSM Animator/LogicRoot/---Boss---/Boss_Yi Gung/States/";
        
        public const string ATTACK_PATH = STATES_PATH + "Attacks/";
        
        public const string ATTACK1_SLOW_STARTER = "[1] Starter  Slow Attack 慢刀前揮";
        public const string ATTACK1_SLOW_STARTER_PHASE_0 = ATTACK1_SLOW_STARTER + "/phase (0)";
        public const string ATTACK2_TELEPORT_TO_TOP = "[2] Teleport To Top";
        public const string ATTACK3_THRUST_DELAY = "[3] Thrust Delay 一閃";
        public const string ATTACK4_SLASH_UP = "[4] Slash Up 上撈下打 大反危";
        public const string ATTACK5_TELEPORT_TO_BACK = "[5] Teleport to Back";
        public const string ATTACK6_DOUBLE_ATTACK = "[6] Double Attack";
        public const string ATTACK6_DOUBLE_ATTACK_WEIGHT = ATTACK6_DOUBLE_ATTACK + "/weight";
        public const string ATTACK7_TELEPORT_FORWARD = "[7] Teleport Dash Forward";
        public const string ATTACK8_LONG_CHARGE = "[8] Long Charge (2階才有";
        public const string ATTACK9_STARTER = "[9] Starter";
        public const string ATTACK9_STARTER_PHASE_0 = ATTACK9_STARTER + "/phase (0)";
        public const string ATTACK10_FOO = "[10] Danger Foo Grab";
        public const string ATTACK11_GIANT_CHARGE_WAVE = "[11] GiantChargeWave 紅白白紅";
        public const string ATTACK12_SLASH_UP_CRIMSON = "[12] UpSlash Down Danger";
        public const string ATTACK13_TRIPLE_POKE = "[13] Tripple Poke 三連";
        public const string ATTACK13_TRIPLE_POKE_WEIGHT = ATTACK13_TRIPLE_POKE + "/weight";
        public const string ATTACK14_CRIMSON_BALL = "[14] FooExplode Smash 下砸紅球";
        public const string ATTACK15_TURN_AROUND_BRIGHT_EYES = "[15] TurnAroundPrepare";
        public const string ATTACK15_TURN_AROUND_BRIGHT_EYES_WEIGHT = ATTACK15_TURN_AROUND_BRIGHT_EYES + "/weight";
        public const string ATTACK16_QUICK_FOO = "[16] QuickFoo";
        public const string ATTACK17_CRIMSON_SLAM = "[17] DownAttack Danger 空中下危";
        public const string ATTACK17_CRIMSON_SLAM_WEIGHT = ATTACK17_CRIMSON_SLAM + "/weight";
        public const string ATTACK18_TELEPORT_TO_BACK_COMBO = "[18] Teleport to Back Combo";
        public const string ATTACK19_THRUST_FULL_SCREEN = "[19] Thrust Full Screen Slash";
        public const string ATTACK20_TELEPORT_OUT = "[20] TeleportOut";
        public const string POSTURE_BREAK = "PostureBreak";
        public const string POSTURE_BREAK_PHASE_0 = POSTURE_BREAK + "/phase0";
        public const string JUMP_BACK = "JumpBack";
        public const string JUMP_BACK_WEIGHT = JUMP_BACK + "/weight";
        public const string ATTACK_PARRYING = "AttackParrying";
        public const string ATTACK_PARRYING_WEIGHT = ATTACK_PARRYING + "/weight";
    }
    
    public static class EigongAttackAnimationRefs
    {
        public const string EIGONG_FOO_ATTACK_1 = "Attack10";
        public const string EIGONG_FOO_ATTACK_2 = "Attack16";
        public const string EIGONG_CRIMSON_BALL_ATTACK = "Attack14";
    }
    
    public static class EigongTitle
    {
        public const string EIGONG_TITLE = "Promised Eigong";
    }
    
    public static class EigongHealth
    {
        public const int EIGONG_PHASE_1_HEALTH_VALUE = 5555;
        public const int EIGONG_PHASE_2_HEALTH_VALUE = 7777;
        public const int EIGONG_PHASE_3_HEALTH_VALUE = 11111;
    }

    public static class EigongDamageReduction
    {
        public const float EIGONG_SPIKED_ARROW_DR = 0.9f;
        public const float EIGONG_NORMAL_AND_ELECTRIC_ARROW_DR = 0.77f;
        public const float EIGONG_CHARGE_ATTACK_DR = 0.66f;
        public const float EIGONG_THIRD_ATTACK_JADE_DR = 0.33f;
        public const float EIGONG_POSTURE_DAMAGE_DR = 0.33f;
        public const float EIGONG_COUNTER_JADE_DAMAGE_DR = 0.33f;
        public const float EIGONG_FOO_DR = 0.2f;
        public const float EIGONG_DEFAULT_DR = 0;
    }

    public static class EigongDamageBoost
    {
        public const float EIGONG_FIRE_BOOST = 1 + 3.33f;
        public const float EIGONG_FOO_BOOST = 1 + 1.33f;
        public const float EIGONG_CRIMSON_BALL_BOOST = 1 + 7.77f;
        public const float EIGONG_DEFAULT_ATTACK_BOOST = 1 + 0.33f;
        public const float EIGONG_EARLY_DEFLECT_BOOST = 1 + 1.77f;
    }
    
    public static class EigongSpeed
    {
        public const float ATTACK1_SLOW_STARTER_SPEED = 1 + 0.7f;
        public const float ATTACK2_TELEPORT_TO_TOP_SPEED = 1 + 0.55f;
        public const float ATTACK3_THRUST_DELAY_SPEED = 1 + 0.5f;
        public const float ATTACK5_TELEPORT_TO_BACK_SPEED = 1 + 0.55f;
        public const float ATTACK6_DOUBLE_ATTACK_SPEED = 1 + 0.33f;
        public const float ATTACK7_TELEPORT_FORWARD_SPEED = 1 + 0.55f;
        public const float ATTACK9_STARTER_SPEED = 1 - 0.11f;
        public const float ATTACK10_FOO_SPEED = 1 + 0.42f;
        public const float ATTACK11_GIANT_CHARGE_WAVE_SPEED = 1 + 0.5f;
        public const float ATTACK13_TRIPLE_POKE_SPEED = 1 + 0.27f;
        public const float ATTACK14_CRIMSON_BALL_SPEED = 1 + 0.33f;
        public const float ATTACK15_TURN_AROUND_BRIGHT_EYES_SPEED = 1 + 0.5f;
        public const float ATTACK16_QUICK_FOO_SPEED = 1 + 0.17f;
        public const float ATTACK17_CRIMSON_SLAM_SPEED = 1 + 0.33f;
        public const float JUMP_BACK_SPEED = 1 + 0.55f;

    }
}