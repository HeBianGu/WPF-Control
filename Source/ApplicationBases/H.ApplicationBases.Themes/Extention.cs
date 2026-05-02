// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ApplicationBases.Themes;
using H.Extensions.FontIcon;
using H.Modules.Theme;
using H.Themes.Colors.Accent;
using H.Themes.Colors.Blue;
using H.Themes.Colors.Copper;
using H.Themes.Colors.Gray;
using H.Themes.Colors.Industrial;
using H.Themes.Colors.Mineral;
using H.Themes.Colors.Platform;
using H.Themes.Colors.Purple;
using H.Themes.Colors.Solid;
using H.Themes.Colors.Technology;
using H.Themes.Colors.Web;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDefaultThemeServices(this IServiceCollection services, Action<IDefaultThemeOptions> option = null)
        {
            DefaultThemeOptions opt = new DefaultThemeOptions();
            option?.Invoke(opt);
            //services.AddSwitchThemeViewPresenter(opt.GetConfigOptions<Action<ISwitchThemeOptions>>());
            //services.AddLoadThemeOptionsService(opt.GetConfigOptions<Action<IThemeOptions>>());
            services.AddTheme(opt.GetConfigOptions<Action<IThemeOptions>>());
            services.AddColorThemeViewPresenter(opt.GetConfigOptions<Action<IColorThemeOptions>>());
        }

        public static void UseDefaultThemeOptions(this IApplicationBuilder app, Action<IDefaultThemeOptions> option = null)
        {
            DefaultThemeOptions opt = new DefaultThemeOptions();
            option?.Invoke(opt);
            app.UseDefaultColorResources(opt.GetConfigOptions<Action<IColorThemeOptions>>());
            app.UseDefaultIconFontFamilys(opt.GetConfigOptions<Action<IIconFontFamilysOptions>>());
        }

        public static void UseDefaultColorResources(this IApplicationBuilder app, Action<IColorThemeOptions> option = null)
        {
            app.UseThemeOptions(x =>
            {
                x.ColorResources.Add(new GrayDarkColorResource());
                x.ColorResources.Add(new GrayLightColorResource());
                x.ColorResources.Add(new BlueDarkColorResource());
                x.ColorResources.Add(new BlueLightColorResource());
                x.ColorResources.Add(new PurpleDarkColorResource());
                x.ColorResources.Add(new PurpleLightColorResource());
                x.ColorResources.Add(new AccentBlueDarkColorResource());
                x.ColorResources.Add(new AccentBlueLightColorResource());
                x.ColorResources.Add(new AccentDefaultDarkColorResource());
                x.ColorResources.Add(new AccentDefaultLightColorResource());
                x.ColorResources.Add(new CopperColorResource());
                x.ColorResources.Add(new VintageFilmColorResource());
                x.ColorResources.Add(new OracleDarkColorResource());
                x.ColorResources.Add(new VikingDarkColorResource());
                x.ColorResources.Add(new ChambrayDarkColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.ExecutiveBlueColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.EnterpriseGrayColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.FinanceNavyColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.LegalInkColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.MedicalCleanColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.ResearchPaperColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.CoffeeCreamColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.VintagePaperColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.SunsetCoralColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.AutumnMapleColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.WarmAmberColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.BookstoreColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.CinnamonColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.OldFilmColorResource());
                x.ColorResources.Add(new ForestMistColorResource());
                x.ColorResources.Add(new MineralColorResource());
                x.ColorResources.Add(new OceanBreezeColorResource());
                x.ColorResources.Add(new MountainSlateColorResource());
                x.ColorResources.Add(new MossGreenColorResource());
                x.ColorResources.Add(new ClayWarmColorResource());
                x.ColorResources.Add(new DesertSandColorResource());
                x.ColorResources.Add(new LakeMorningColorResource());
                x.ColorResources.Add(new RainyDayColorResource());
                x.ColorResources.Add(new CyberVioletColorResource());
                x.ColorResources.Add(new NeuralBlueColorResource());
                x.ColorResources.Add(new QuantumTealColorResource());
                x.ColorResources.Add(new DataStreamColorResource());
                x.ColorResources.Add(new HolographicColorResource());
                x.ColorResources.Add(new AIConsoleColorResource());
                x.ColorResources.Add(new DeepSpaceColorResource());
                x.ColorResources.Add(new SatelliteOpsColorResource());
                x.ColorResources.Add(new TechnologyBlueDarkColorResource());
                x.ColorResources.Add(new TechnologyPurpleColorResource());
                x.ColorResources.Add(new FuturisticGreenDarkColorResource());
                x.ColorResources.Add(new AmberTerminalDarkColorResource());
                x.ColorResources.Add(new FuturismColorResource());
                x.ColorResources.Add(new CyberpunkColorResource());
                x.ColorResources.Add(new TechnologyCyanColorResource());
                x.ColorResources.Add(new TechnologyIndigoColorResource());
                x.ColorResources.Add(new TechnologySOIORColorResource());
                x.ColorResources.Add(new TechnologyPinkColorResource());
                x.ColorResources.Add(new FactorySteelColorResource());
                x.ColorResources.Add(new IndustrialDarkColorResource());
                x.ColorResources.Add(new SafetyOrangeColorResource());
                x.ColorResources.Add(new MachineGreenColorResource());
                x.ColorResources.Add(new WarningAmberColorResource());
                x.ColorResources.Add(new WorkshopDarkColorResource());
                x.ColorResources.Add(new ControlRoomColorResource());
                x.ColorResources.Add(new AIVisionDarkColorResource());
                x.ColorResources.Add(new AnnotationBlueColorResource());
                x.ColorResources.Add(new TrainingConsoleColorResource());
                x.ColorResources.Add(new DatasetManagerColorResource());
                x.ColorResources.Add(new SecurityOpsColorResource());
                x.ColorResources.Add(new MedicalImageColorResource());
                x.ColorResources.Add(new GeoMapDarkColorResource());
                x.ColorResources.Add(new LabInstrumentColorResource());
                x.ColorResources.Add(new EnergyGridColorResource());
                x.ColorResources.Add(new TrafficControlColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ChartOceanColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ChartCandyColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ChartScientificColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ChartTrafficColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ChartEnergyColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ChartThermalColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.DashboardNightColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.MonitorGreenColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.MetricBlueColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.AnalyticsPurpleColorResource());
                x.ColorResources.Add(new ModernFluentColorResource());
                x.ColorResources.Add(new MaterialSoftColorResource());
                x.ColorResources.Add(new CupertinoLightColorResource());
                x.ColorResources.Add(new GitDarkColorResource());
                x.ColorResources.Add(new TerminalClassicColorResource());
                x.ColorResources.Add(new DashboardBlueColorResource());
                x.ColorResources.Add(new CloudConsoleColorResource());
                x.ColorResources.Add(new IDEBlueDarkColorResource());
                x.ColorResources.Add(new EditorDarkPlusColorResource());
                x.ColorResources.Add(new SolarizedLightColorResource());
                x.ColorResources.Add(new SolarizedDarkColorResource());
                x.ColorResources.Add(new AppleColorResource());
                x.ColorResources.Add(new FluentUIColorResource());
                x.ColorResources.Add(new MaterialDesignColorResource());
                x.ColorResources.Add(new AntDesignProColorResource());
                x.ColorResources.Add(new BootstrapColorResource());
                x.ColorResources.Add(new LayUIColorResource());
                x.ColorResources.Add(new WebLightUIColorResource());
                x.ColorResources.Add(new WeUIColorResource());
                x.ColorResources.Add(new ColorUIGAColorResource());
                x.ColorResources.Add(new WeChatStyleColorResource());
                x.ColorResources.Add(new QQStyleColorResource());
                x.ColorResources.Add(new AlipayStyleColorResource());
                x.ColorResources.Add(new TaobaoStyleColorResource());
                x.ColorResources.Add(new TmallStyleColorResource());
                x.ColorResources.Add(new XianyuStyleColorResource());
                x.ColorResources.Add(new JDStyleColorResource());
                x.ColorResources.Add(new WeComStyleColorResource());
                x.ColorResources.Add(new CSDNStyleColorResource());
                x.ColorResources.Add(new MeituanStyleColorResource());
                x.ColorResources.Add(new DianpingStyleColorResource());
                x.ColorResources.Add(new YoukuStyleColorResource());
                x.ColorResources.Add(new TencentVideoStyleColorResource());
                x.ColorResources.Add(new IQIYIStyleColorResource());
                x.ColorResources.Add(new WPSOfficeStyleColorResource());
                x.ColorResources.Add(new MicrosoftOfficeStyleColorResource());
                x.ColorResources.Add(new QihooSecurityStyleColorResource());
                x.ColorResources.Add(new KugouStyleColorResource());
                x.ColorResources.Add(new BilibiliStyleColorResource());
                x.ColorResources.Add(new HikVisionMasterStyleColorResource());
                x.ColorResources.Add(new VS2022BlueStyleColorResource());
                x.ColorResources.Add(new VS2022DarkStyleColorResource());
                x.ColorResources.Add(new VS2022LightStyleColorResource());
                x.ColorResources.Add(new HonorOfKingsStyleColorResource());
                x.ColorResources.Add(new EggyPartyStyleColorResource());
                x.ColorResources.Add(new PeaceEliteStyleColorResource());
                x.ColorResources.Add(new DouyinStyleColorResource());
                x.ColorResources.Add(new KuaishouStyleColorResource());
                x.ColorResources.Add(new AuroraOpsColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.PaperMintColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.WarmStudioColorResource());
                x.ColorResources.Add(new NeonMatrixColorResource());
                x.ColorResources.Add(new CyberSunsetColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ThermalInsightColorResource());
                x.ColorResources.Add(new OceanSignalColorResource());
                x.ColorResources.Add(new BioTechLabColorResource());
                x.ColorResources.Add(new ModelForgeColorResource());
                x.ColorResources.Add(new TensorFlowColorResource());
                x.ColorResources.Add(new NeuralBloomColorResource());
                x.ColorResources.Add(new InferenceNightColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.AnnotationMintColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.LabelStudioColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.DatasetSlateColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.ImageLabColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.FocusPaperColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.CalmDeskColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.WarmNotebookColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Office.SoftDashboardColorResource());
                x.ColorResources.Add(new GraphitePulseColorResource());
                x.ColorResources.Add(new CommandCenterColorResource());
                x.ColorResources.Add(new ProductionLineColorResource());
                x.ColorResources.Add(new AlarmBoardColorResource());
                x.ColorResources.Add(new QualityGateColorResource());
                x.ColorResources.Add(new OpsRadarColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.InsightPrismColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.ReportCanvasColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.MetricAuroraColorResource());
                x.ColorResources.Add(new H.Themes.Colors.Dashboard.RiskHeatmapColorResource());

                x.ColorResources.Add(new MidnightNavyDarkColorResource());
                x.ColorResources.Add(new EmeraldDarkColorResource());
                x.ColorResources.Add(new CrimsonDarkColorResource());
                x.ColorResources.Add(new AmberDarkColorResource());
                x.ColorResources.Add(new RoyalVioletDarkColorResource());
                x.ColorResources.Add(new TealDarkColorResource());
                x.ColorResources.Add(new GraphiteDarkColorResource());
                x.ColorResources.Add(new CoffeeDarkColorResource());
                x.ColorResources.Add(new CyberLimeDarkColorResource());
                x.ColorResources.Add(new IndigoDarkColorResource());
                x.ColorResources.Add(new ArcticLightColorResource());
                x.ColorResources.Add(new MintLightColorResource());
                x.ColorResources.Add(new CoralLightColorResource());
                x.ColorResources.Add(new LavenderLightColorResource());
                x.ColorResources.Add(new RoseLightColorResource());
                x.ColorResources.Add(new SandLightColorResource());

                x.ColorResources.Add(new NordDarkColorResource());
                x.ColorResources.Add(new DraculaDarkColorResource());
                x.ColorResources.Add(new OceanAbyssDarkColorResource());
                x.ColorResources.Add(new WineDarkColorResource());
                x.ColorResources.Add(new ForestDarkColorResource());
                x.ColorResources.Add(new TerminalGreenDarkColorResource());
                x.ColorResources.Add(new PearlLightColorResource());
                x.ColorResources.Add(new SkyLightColorResource());
                x.ColorResources.Add(new SageLightColorResource());
                x.ColorResources.Add(new PeachLightColorResource());
                x.ColorResources.Add(new AquaLightColorResource());
                x.ColorResources.Add(new CloudLightColorResource());

                x.ColorResources.Add(new NeonCyanDarkColorResource());
                x.ColorResources.Add(new QuantumPurpleDarkColorResource());
                x.ColorResources.Add(new MatrixGreenDarkColorResource());
                x.ColorResources.Add(new HologramBlueDarkColorResource());
                x.ColorResources.Add(new StarshipOrangeDarkColorResource());
                x.ColorResources.Add(new AIMagentaDarkColorResource());
                x.ColorResources.Add(new DataCenterDarkColorResource());
                x.ColorResources.Add(new LaserRedDarkColorResource());
                x.ColorResources.Add(new LabBlueLightColorResource());
                x.ColorResources.Add(new FutureWhiteLightColorResource());
                x.ColorResources.Add(new CircuitCyanLightColorResource());
                x.ColorResources.Add(new RobotGrayLightColorResource());

                x.ColorResources.Add(new AuroraCircuitDarkColorResource());
                x.ColorResources.Add(new SynthwaveGridDarkColorResource());
                x.ColorResources.Add(new QuantumSunsetDarkColorResource());
                x.ColorResources.Add(new PlasmaStormDarkColorResource());
                x.ColorResources.Add(new BioCircuitDarkColorResource());
                x.ColorResources.Add(new IceFireCoreDarkColorResource());
                x.ColorResources.Add(new NebulaOpsDarkColorResource());
                x.ColorResources.Add(new SolarFlareDarkColorResource());
                x.ColorResources.Add(new AuroraGlassLightColorResource());
                x.ColorResources.Add(new NeonPastelLightColorResource());
                x.ColorResources.Add(new QuantumLabLightColorResource());
                x.ColorResources.Add(new HoloMintLightColorResource());
                option?.Invoke(x);
            });
        }

        public static void UseDefaultIconFontFamilys(this IApplicationBuilder app, Action<IIconFontFamilysOptions> option = null)
        {
            app.UseThemeOptions(x =>
            {
                option?.Invoke(x);
            });
        }
    }
}
