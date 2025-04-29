namespace PromisedEigong;

public static class PromisedEigongModGlobalSettings
{
    public static class EigongLocs
    {
        public const string EIGONG_TITLE_LOC = "Characters/NameTag_YiKong"; 
    }

    public static class EigongRefs
    {
        public const string SCENE_NORMAL_ENDING_EIGONG = "A11_S0_Boss_YiGung";
        public const string SCENE_TRUE_ENDING_EIGONG = "A11_S0_Boss_YiGung_回蓬萊";
        public const string BIND_MONSTER_EIGONG_NAME = "Boss_Yi Gung";
    }
    
    public static class EigongAttackRefs
    {
        public const string EIGONG_TALISMAN_ATTACK_1 = "Attack10";
        public const string EIGONG_TALISMAN_ATTACK_2 = "Attack16";
    }
    
    public static class EigongTitle
    {
        public const string EIGONG_TITLE = "Promised Eigong";
    }
    
    public static class EigongHealth
    {
        public const int EIGONG_PHASE_1_HEALTH_VALUE = 6000;
        public const int EIGONG_PHASE_2_HEALTH_VALUE = 9001;
        public const int EIGONG_PHASE_3_HEALTH_VALUE = 11000;
    }

    public static class EigongDamageReduction
    {
        public const float EIGONG_NORMAL_AND_ELECTRIC_ARROW_DR = 0.77f;
        public const float EIGONG_SPIKED_ARROW_DR = 0.9f;
        public const float EIGONG_CHARGE_ATTACK_DR = 0.66f;
        public const float EIGONG_THIRD_ATTACK_JADE_DR = 0.5f;
        public const float EIGONG_FOO_DR = 0.2f;
        public const float EIGONG_POSTURE_DAMAGE_DR = 0.33f;
        public const float EIGONG_COUNTER_JADE_DAMAGE_DR = 0.33f;
        public const float EIGONG_DEFAULT_DR = 0;
    }

    public static class EigongDamageBoost
    {
        public const float EIGONG_FIRE_BOOST = 1 + 3.33f;
        public const float EIGONG_TALISMAN_BOOST = 1 + 1.33f;
    }
}