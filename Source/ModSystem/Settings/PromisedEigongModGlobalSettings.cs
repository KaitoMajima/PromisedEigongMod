namespace PromisedEigong;

using BlendModes;
using UnityEngine;

public static class PromisedEigongModGlobalSettings
{
    public static class EigongLocs
    {
        public const string EIGONG_TITLE_LOC = "Characters/NameTag_YiKong"; 
        public const string ROOT_PROGRESS_LOC = "AG_ST_Hub/M322_AG_ST_古樹解封進度_Bubble00"; 
    }

    public static class EigongRefs
    {
        public const string SCENE_NORMAL_ENDING_EIGONG = "A11_S0_Boss_YiGung_回蓬萊";
        public const string SCENE_TRUE_ENDING_EIGONG = "A11_S0_Boss_YiGung";
        public const string SCENE_NEW_KUNLUN = "AG_ST_Hub";
        
        public const string BIND_MONSTER_EIGONG_NAME = "Boss_Yi Gung";

        public const string BG_MASTER_TRANSFORM =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/3DBG ScaleCompress/Scene BG_YiGungBoosFight";
        
        public const string FX_CAMERA_PATH =
            "GameLevel/CameraCore/DockObj/OffsetObj/ShakeObj/SceneCamera/AmplifyLightingSystem/FxCamera";
        
        public const string BEAUTIFUL_FX_CAMERA_NAME = "BeautifulFxCamera";
        
        public const string BEAUTIFUL_FX_CAMERA_PATH =
            "GameLevel/CameraCore/DockObj/OffsetObj/ShakeObj/SceneCamera/AmplifyLightingSystem/" + BEAUTIFUL_FX_CAMERA_NAME;
        
        public const string CAMERA_FX_MASTER_TRANSFORM =
            "GameLevel/CameraCore/DockObj/OffsetObj/ShakeObj/SceneCamera/AmplifyLightingSystem";

        public const string BOSS_AMBIENCE_SOURCE =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/FSM Animator/LogicRoot/Boss三階BGM/BGM_Boss_A11_P3";
        
        public const string EIGONG_TITLE_NAME_PATH =
            "GameCore(Clone)/RCG LifeCycle/UIManager/GameplayUICamera/MonsterHPRoot/BossHPRoot/UIBossHP(Clone)/Offset(DontKeyAnimationOnThisNode)/AnimationOffset/BossName";

        public const string EIGONG_CUTSCENE_TITLE_NAME_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/FSM Animator/[CutScene] 一進/Boss Intro Acting 字卡/View/Boss_YiGung/Subtitle_Text/Title";

        public const string PO_VFX_TAI_DANGER = "Effect_TaiDanger(Clone)";
        public const string PO_VFX_JIECHUAN_FIRE = "Fire_FX_damage_Long jiechuan(Clone)";
        public const string PO_VFX_JIECHUAN_FIRE_IMAGE = "Light";
        public const string PO_VFX_EIGONG_FOO_EXPLOSION = "Fx_YiGong Foo Explosion_pool obj(Clone)";
        public const string PO_VFX_EIGONG_FOO = "Fx_YiGong 貼符(Clone)";
        public const string PO_VFX_EIGONG_CRIMSON_GEYSER = "Fx_YiGong Upper12_pool obj(Clone)";
        public const string PO_VFX_EIGONG_CRIMSON_PILLAR = "FireExplosionPillar_FX_damage(Clone)";
    }

    public static class EigongPreloadRefs
    {
        public const string BIG_BAD_SCENE = "結局演出_大爆炸 P2";
        public const string BIG_BAD_PATH = "GameLevel/Room/3DBG Master 背景/BIGBAD";

        public const string BEAUTIFUL_FX_CAMERA_PRELOAD_PATH =
            "GameLevel/CameraCore/DockObj/OffsetObj/ShakeObj/SceneCamera/AmplifyLightingSystem/FxCamera";
    }

    public static class EigongBackground
    {
        public const string RED_FX = "GameLevel/CameraCore/DockObj/全畫面遮色/Fade/Red";
        public const string GREEN_FX = "GameLevel/CameraCore/DockObj/全畫面遮色/Fade/Green";
        public const string BLACK_FX = "GameLevel/CameraCore/DockObj/全畫面遮色/Fade/Black";
        public const string BLACK_2_FX = "GameLevel/CameraCore/DockObj/全畫面遮色/Fade/Black/Black (1)";
        public const string BLUE_FX = "GameLevel/CameraCore/DockObj/全畫面遮色/Fade/Blue";
        public const string DONUT_FX = "GameLevel/CameraCore/DockObj/全畫面遮色/Fade/Donut (1)";
        public const string PLATFORM_LIGHT_FX =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/3DBG ScaleCompress/Scene BG_YiGungBoosFight/Directional Light";
        public const string ANIMATOR_REF = "GameLevel/CameraCore/DockObj/全畫面遮色";
        
        public const string GIANT_BALL =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/STGiantBall";
        
        public const string CORE_VINES_ROOT =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/3DBG ScaleCompress/Scene BG_YiGungBoosFight/showdownscene3d_Vines";
        
        public const string CORE_VINES_PARENT =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/3DBG ScaleCompress/Scene BG_YiGungBoosFight/showdownscene3d_Vines/COREVINES_脫殼中";
        
        public const string CORE_VINES_INFECTED_PARENT =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/3DBG ScaleCompress/Scene BG_YiGungBoosFight/showdownscene3d_Vines/COREVINES_感染前";
        
        public const string CORE_VINES_INFECTED_PARENT_FINAL =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/3DBG ScaleCompress/Scene BG_YiGungBoosFight/showdownscene3d_Vines/COREVINES_感染後";

        public const string BIG_BAD_OBJ =
            "GameLevel/Room/3DBG Master 背景/3D 大背景/3DBG ScaleCompress/Scene BG_YiGungBoosFight/BIGBAD(Clone)(Clone)";
        
        public const string BIG_BAD_ANIMATION =
            BIG_BAD_OBJ + "/BIGBAD_ANIM";
        public const string BIG_BAD_NECKVINES =
            BIG_BAD_OBJ + "/BIGBAD_ANIM/MORPH_GUYS/NECKVINES";
        public const string BIG_BAD_NECKVINES_PARENT =
            BIG_BAD_OBJ + "/BIGBAD_ANIM/MORPH_GUYS";
        public const string BIG_BAD_STATIC_OBJS =   
            BIG_BAD_OBJ + "/BIGBAD_ANIM/STATIC_OBJS";
        public const string BIG_BAD_STATIC_OBJS_PARENT =
            BIG_BAD_OBJ + "/BIGBAD_ANIM/STATIC_OBJS";
        public const string BIG_BAD_MEATBALL_1 =
            BIG_BAD_OBJ + "/HighEndFX(Disable for Switch)/P_MeatBall (1)";
        public const string BIG_BAD_MEATBALL_2 =
            BIG_BAD_OBJ + "/HighEndFX(Disable for Switch)/P_MeatBall (2)";
        public const string BIG_BAD_MEATBALL_PARENT =
            BIG_BAD_OBJ + "/HighEndFX(Disable for Switch)";

        public const string BIG_BAD_HEAD =
            BIG_BAD_OBJ + "/BIGBAD_ANIM/MORPH_GUYS/HEAD";
        
        public const string BIG_BAD_HAIR =
            BIG_BAD_OBJ + "/BIGBAD_HAIRS";

        public static readonly Color RED_COLOR = new(1f, 0f, 0.0f, 0.3129f);
        public static readonly Color RED_COLOR_PHASE_3 = new(1f, 0f, 0.5856f, 0.2929f);
        public static readonly Color GREEN_COLOR = new( 0.607f, 0f, 0f, 0.4128f);
        public static readonly Color GREEN_COLOR_PHASE_3 = new( 1f, 0f, 0.6f, 0.4128f);
        public static readonly Color BLACK_COLOR = new( 0f, 0f, 0f, 1f);
        public static readonly Color BLACK_2_COLOR = new( 0.2662f, 0.2f, 0f, 0.7128302f);
        public static readonly Color BLACK_2_COLOR_PHASE_3 = new( 0f, 0.3956f, 0.1707547f, 0.5528302f);
        public static readonly Color BLUE_COLOR = new( 0.6232f, 0f, 0.4174f, 1f);
        public static readonly Color BLUE_COLOR_PHASE_3 = new( 0.3168f, 0f, 0.5476f, 1f);
        public static readonly Color DONUT_COLOR = new( 0.2075f, 0f, 0.1193f, 0.2424f);
        public static readonly Color DONUT_COLOR_PHASE_3 = new( 0.6275f, 0f, 0.1193f, 0.2324f);
        public static readonly Color PLATFORM_LIGHT_COLOR = new( 0.5664f, 0.5202f, 0.3828f, 1f);

        public static readonly BlendMode RED_BLEND_MODE = BlendMode.LinearDodge;
        public static readonly BlendMode BLUE_BLEND_MODE = BlendMode.SoftLight;
        public static readonly BlendMode BLUE_BLEND_MODE_PHASE_2 = BlendMode.SoftLight;

        public static readonly Vector3 BIG_BAD_NECKVINES_ROTATION = new(0, 180, 0);
        public static readonly Vector3 BIG_BAD_STATIC_OBJS_POSITION = new(0, 0, 48.4f);
        public static readonly Vector3 BIG_BAD_STATIC_OBJS_ROTATION = new(0, 187.01F, 0);
        public static readonly Vector3 BIG_BAD_MEATBALL_ROTATION = new(0, 180, 0);
        public static readonly Vector3 BIG_BAD_MEATBALL_2_SHAPE_POSITION = new(0, 17, 0);
        public static readonly Vector3 BIG_BAD_MEATBALL_2_SHAPE_SCALE = new(4.7f, 4, 4.7f);
        public static readonly Vector3 BIG_BAD_HEAD_SCALE = new(0.97f, 1.05f, 0.97f);
        public static readonly Vector3 BIG_BAD_HEAD_POSITION = new(0, 70, 0);

        public const float GIANT_BALL_SPEED_1 = 25;
        public const float CORE_VINES_SPEED_1 = 9f;
        public const float GIANT_BALL_SPEED_2 = 0.25f;
        public const float BIG_BAD_HEAD_SPEED = 3.5f;
        public const float BEAUTIFUL_FX_CAMERA_WEIGHT = 0.77f;
    }

    public static class EigongOST
    {
        public const string PHASE1_2_OST = "BGM_Boss_A11_P3";
        public const string PHASE3_OST = "BGM_EndCredit_NineSols";
    }
    
    public static class EigongVFX
    {
        public const string DANGER_VFX_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/" +
            "FSM Animator/LogicRoot/---Boss---/Boss_Yi Gung/MonsterCore/Animator(Proxy)/Animator/LogicRoot/DangerHintEffect";

        public const string SIMPLE_DANGER_VFX_NAME = "SimpleEffect_TaiDanger";
        public static readonly Vector3 SIMPLE_DANGER_VFX_OFFSET_L = new(-74.7f, 73, 0);
        public static readonly Vector3 SIMPLE_DANGER_VFX_OFFSET_R = new(74.7f, 73, 0);
        public static readonly Vector3 SIMPLE_DANGER_VFX_SCALE = new(0.72f, 0.42f, 0.42f);
        public const float SIMPLE_DANGER_VFX_VALUE_BRIGHTNESS = 9;
        public const float SIMPLE_DANGER_VFX_SATURATION = 0.42f;
        public const float SIMPLE_DANGER_VFX_HIDE_TIME = 0.18f;
    }

    public static class EigongSFX
    {
        public const string SIMPLE_DANGER_SFX_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/FSM Animator/LogicRoot/---Boss---/" +
            "Boss_Yi Gung/MonsterCore/Animator(Proxy)/Animator/LogicRoot/Audio/EnemySFX_YiGong_AirSwords_OpenEye";

        public const int SIMPLE_DANGER_SFX_BOOST = 2;
    }

    public static class NewKunlunBackground
    {
        public const string PROGRESS_BAR = 
            "AG_ST_Hub/Room/Prefab/SimpleCutSceneFSM_古樹核心解封程度/" +
            "FSM Animator/View/HubSPWall/BigScreen/CONTENT/80/進度/進度條";

        public const float BAR_WIDTH = 89;
        public static readonly Color BAR_COLOR = new(1, 0.3f, 0.3f);
    }

    public static class EigongColors
    {
        public const string EIGONG_CHARACTER_VIEW_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/" +
            "FSM Animator/LogicRoot/---Boss---/Boss_Yi Gung/MonsterCore/Animator(Proxy)/Animator/View/YiGung";
        
        public const string EIGONG_CUTSCENE_VIEW_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/" +
            "FSM Animator/[CutScene] 二進/YiGung_Dummy/View/YiGung";

        public const string FIRE_TRAIL_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/" +
            "FSM Animator/LogicRoot/---Boss---/Boss_Yi Gung/MonsterCore/Animator(Proxy)/Animator/LogicRoot/Phase1 Activator/FireFX _ Fxplayer";
            
        public const string EIGONG_CHARACTER_BODY = EIGONG_CHARACTER_VIEW_PATH + "/Body";
        public const string EIGONG_CHARACTER_BODY_SHADOW = EIGONG_CHARACTER_BODY + "/Shadow";
        public const string EIGONG_CHARACTER_SWORD = EIGONG_CHARACTER_VIEW_PATH + "/Weapon/Sword/Sword Sprite";
        public const string EIGONG_CHARACTER_SWORD_EFFECT = EIGONG_CHARACTER_VIEW_PATH + "/Weapon/Sword/Effect";
        public const string EIGONG_CHARACTER_CRIMSON_SLAM_EFFECT = EIGONG_CHARACTER_VIEW_PATH + "/Attack Effect/Upper4/Sprite";
        public const string EIGONG_CHARACTER_CRIMSON_GEYSER_EFFECT = EIGONG_CHARACTER_VIEW_PATH + "/Attack Effect/Upper12/Sprite";
        public const string EIGONG_CHARACTER_FOO = EIGONG_CHARACTER_VIEW_PATH + "/Weapon/Foo/FooSprite";
        public const string EIGONG_CHARACTER_FOO_EFFECT_1 = EIGONG_CHARACTER_VIEW_PATH + "/Weapon/Foo/P_spark Up";
        public const string EIGONG_CHARACTER_FOO_EFFECT_2 = EIGONG_CHARACTER_VIEW_PATH + "/Weapon/Foo/P_spark Circle";
        public const string EIGONG_CHARGE_WAVE_CRIMSON = EIGONG_CHARACTER_VIEW_PATH + "/Attack Effect/紅白白紅/紅/AttackEffect";
        public const string EIGONG_CHARGE_WAVE_CRIMSON_GLOW = EIGONG_CHARACTER_VIEW_PATH + "/Attack Effect/紅白白紅/紅/Glow";
        public const string EIGONG_CHARGE_WAVE_CRIMSON_2 = EIGONG_CHARACTER_VIEW_PATH + "/Attack Effect/紅白白紅/究極紅/AttackEffect";
        public const string EIGONG_CHARGE_WAVE_CRIMSON_2_GLOW = EIGONG_CHARACTER_VIEW_PATH + "/Attack Effect/紅白白紅/究極紅/Glow";
        public const string EIGONG_CUTSCENE_BODY = EIGONG_CUTSCENE_VIEW_PATH + "/Body";
        public const string EIGONG_CUTSCENE_SWORD = EIGONG_CUTSCENE_VIEW_PATH + "/Weapon/Sword/Sword Sprite";

        public static readonly Color FIRE_COLOR = new(0f, 0.77f, 0.99f);
        public static readonly Color FIRE_LIGHT_COLOR = new(0f, 0.77f, 0.99f);
        public const float EIGONG_BURNING_FX_STRENGTH = 0.53f;
        public const float EIGONG_CHARACTER_SWORD_EFFECT_SHIFT = 522;
        public const float EIGONG_SWORD_SATURATION = 5f;
        public const float EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS = 2.2f;
        public const float EIGONG_TAI_DANGER_VALUE_BRIGHTNESS = 5f;
    }
    
    public static class EigongAttacks
    {
        public const string STATES_PATH =
            "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/FSM Animator/LogicRoot/---Boss---/Boss_Yi Gung/States/";
        
        public const string ATTACK_PATH = STATES_PATH + "Attacks/";
        
        public const string ATTACK1_SLOW_STARTER = "[1] Starter  Slow Attack 慢刀前揮";
        public const string ATTACK1_SLOW_STARTER_WEIGHT = ATTACK1_SLOW_STARTER + "/phase (0)";
        public const string ATTACK1_SLOW_STARTER_WEIGHT_PHASE_2 = ATTACK1_SLOW_STARTER + "/phase (1)";
        public const string ATTACK2_TELEPORT_TO_TOP = "[2] Teleport To Top";
        public const string ATTACK2_TELEPORT_TO_TOP_WEIGHT = ATTACK2_TELEPORT_TO_TOP + "/phase (0)";
        public const string ATTACK3_THRUST_DELAY = "[3] Thrust Delay 一閃";
        public const string ATTACK3_THRUST_DELAY_WEIGHT_PHASE_2 = ATTACK3_THRUST_DELAY + "/phase (1)";
        public const string ATTACK4_SLASH_UP = "[4] Slash Up 上撈下打 大反危";
        public const string ATTACK5_TELEPORT_TO_BACK = "[5] Teleport to Back";
        public const string ATTACK5_TELEPORT_TO_BACK_WEIGHT_PHASE_2 = ATTACK5_TELEPORT_TO_BACK + "/weight (1)";
        public const string ATTACK6_DOUBLE_ATTACK = "[6] Double Attack";
        public const string ATTACK6_DOUBLE_ATTACK_WEIGHT = ATTACK6_DOUBLE_ATTACK + "/weight";
        public const string ATTACK7_TELEPORT_FORWARD = "[7] Teleport Dash Forward";
        public const string ATTACK7_TELEPORT_FORWARD_WEIGHT_PHASE_2 = ATTACK7_TELEPORT_FORWARD + "/weight (1)";
        public const string ATTACK8_LONG_CHARGE = "[8] Long Charge (2階才有";
        public const string ATTACK8_LONG_CHARGE_WEIGHT_PHASE_2 = ATTACK8_LONG_CHARGE + "/weight (1)";
        public const string ATTACK9_STARTER = "[9] Starter";
        public const string ATTACK9_STARTER_PHASE_1 = ATTACK9_STARTER + "/phase (0)";
        public const string ATTACK9_STARTER_PHASE_2 = ATTACK9_STARTER + "/phase (1)";
        public const string ATTACK10_FOO = "[10] Danger Foo Grab";
        public const string ATTACK11_GIANT_CHARGE_WAVE = "[11] GiantChargeWave 紅白白紅";
        public const string ATTACK11_GIANT_CHARGE_WAVE_WEIGHT_PHASE_2 = ATTACK11_GIANT_CHARGE_WAVE + "/weight (2)";
        public const string ATTACK12_SLASH_UP_CRIMSON = "[12] UpSlash Down Danger";
        public const string ATTACK12_SLASH_UP_CRIMSON_WEIGHT_PHASE_2 = ATTACK12_SLASH_UP_CRIMSON + "/weight (1)";
        public const string ATTACK13_TRIPLE_POKE = "[13] Tripple Poke 三連";
        public const string ATTACK13_TRIPLE_POKE_WEIGHT = ATTACK13_TRIPLE_POKE + "/weight";
        public const string ATTACK13_TRIPLE_POKE_WEIGHT_PHASE_2 = ATTACK13_TRIPLE_POKE + "/weight (1)";
        public const string ATTACK14_CRIMSON_BALL = "[14] FooExplode Smash 下砸紅球";
        public const string ATTACK15_TURN_AROUND_BRIGHT_EYES = "[15] TurnAroundPrepare";
        public const string ATTACK15_TURN_AROUND_BRIGHT_EYES_WEIGHT_PHASE_2 = ATTACK15_TURN_AROUND_BRIGHT_EYES + "/weight (1)";
        public const string ATTACK16_QUICK_FOO = "[16] QuickFoo";
        public const string ATTACK17_CRIMSON_SLAM = "[17] DownAttack Danger 空中下危";
        public const string ATTACK17_CRIMSON_SLAM_WEIGHT = ATTACK17_CRIMSON_SLAM + "/weight";
        public const string ATTACK17_CRIMSON_SLAM_WEIGHT_PHASE_2 = ATTACK17_CRIMSON_SLAM + "/weight (1)";
        public const string ATTACK18_TELEPORT_TO_BACK_COMBO = "[18] Teleport to Back Combo";
        public const string ATTACK18_TELEPORT_TO_BACK_COMBO_WEIGHT = ATTACK18_TELEPORT_TO_BACK_COMBO + "/weight";
        public const string ATTACK19_THRUST_FULL_SCREEN = "[19] Thrust Full Screen Slash";
        public const string ATTACK20_TELEPORT_OUT = "[20] TeleportOut";
        public const string ATTACK21_NEW_QUICK_CRIMSON_BALL = "[21] Quick Crimson Ball";
        public const string ATTACK22_NEW_INSTANT_CRIMSON_GEYSER = "[22] Instant Crimson Geyser";
        public const string POSTURE_BREAK = "PostureBreak";
        public const string POSTURE_BREAK_PHASE_1 = POSTURE_BREAK + "/phase0";
        public const string POSTURE_BREAK_PHASE_2 = POSTURE_BREAK + "/phase1";
        public const string JUMP_BACK = "JumpBack";
        public const string JUMP_BACK_WEIGHT = JUMP_BACK + "/weight";
        public const string ATTACK_PARRYING = "AttackParrying";
        public const string ATTACK_PARRYING_WEIGHT = ATTACK_PARRYING + "/weight";
        public const string ATTACK_PARRYING_WEIGHT_PHASE_2 = ATTACK_PARRYING + "/weight (1)";
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
        public const string EIGONG_PHASE_3_TITLE = "Tianhuo Avatar";
        public const string ROOT_PROGRESS_TEXT = "Root Decryption Progress: <color=#FF0000>ERROR ERROR 99% ERROR ERROR</color>";
    }
    
    public static class EigongHealth
    {
        public const int EIGONG_PHASE_1_HEALTH_VALUE = 5555;
        public const int EIGONG_PHASE_2_HEALTH_VALUE = 7777;
        public const int EIGONG_PHASE_3_HEALTH_VALUE = 9999;
    }

    public static class EigongDamageReduction
    {
        public const float EIGONG_SPIKED_ARROW_DR = 0.9f;
        public const float EIGONG_NORMAL_AND_ELECTRIC_ARROW_DR = 0.77f;
        public const float EIGONG_CHARGE_ATTACK_DR = 0.66f;
        public const float EIGONG_THIRD_ATTACK_JADE_DR = 0.33f;
        public const float EIGONG_POSTURE_DAMAGE_DR = 0.33f;
        public const float EIGONG_TAICHI_KICK_DAMAGE_DR = 0.5f;
        public const float EIGONG_COUNTER_JADE_DAMAGE_DR = 0.33f;
        public const float EIGONG_FOO_DR = 0;
        public const float EIGONG_DEFAULT_DR = 0;
    }
    
    public static class EigongProperties
    {
        public const bool EIGONG_FORBID_STUNNING = true;
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
        public const float ATTACK4_SLASH_UP_SPEED = 1 + 0.22f;
        public const float ATTACK5_TELEPORT_TO_BACK_SPEED = 1 + 0.55f;
        public const float ATTACK6_DOUBLE_ATTACK_SPEED = 1 + 0.33f;
        public const float ATTACK7_TELEPORT_FORWARD_SPEED = 1 + 0.55f;
        public const float ATTACK8_LONG_CHARGE_SPEED = 1 + 0.77f;
        public const float ATTACK9_STARTER_SPEED = 1 - 0.11f;
        public const float ATTACK10_FOO_SPEED = 1 + 0.42f;
        public const float ATTACK11_GIANT_CHARGE_WAVE_SPEED = 1 + 0.37f;
        public const float ATTACK12_SLASH_UP_CRIMSON_SPEED = 1 + 0.22f;
        public const float ATTACK13_TRIPLE_POKE_SPEED = 1 + 0.27f;
        public const float ATTACK14_CRIMSON_BALL_SPEED = 1 + 0.27f;
        public const float ATTACK15_TURN_AROUND_BRIGHT_EYES_SPEED = 1 + 0.5f;
        public const float ATTACK16_QUICK_FOO_SPEED = 1 + 0.17f;
        public const float ATTACK17_CRIMSON_SLAM_SPEED = 1 + 0.42f;
        public const float ATTACK18_TELEPORT_TO_BACK_COMBO_SPEED = 1 + 0.55f;
        public const float ATTACK21_NEW_INSTANT_CRIMSON_BALL_SPEED = 1 + 0.27f;
        public const float ATTACK21_NEW_INSTANT_CRIMSON_BALL_SKIP = 0.21f;
        public const float JUMP_BACK_SPEED = 1 + 0.55f;
    }
    
    public static class EigongDebug
    {
        public const bool EIGONG_STATE_LOG = false;
        public const bool TEST_YI_INVINCIBLE = false;
        public const bool IS_PAUSE_MENU_BG_INVISIBLE = true;
        
        public const float BASE_HEALTH_MULTIPLIER = 1f;
        public const float PHASE_1_HEALTH_MULTIPLIER = 1f;
        public const float PHASE_2_HEALTH_MULTIPLIER = 1f;
        public const float PHASE_3_HEALTH_MULTIPLIER = 1f;
    }
}