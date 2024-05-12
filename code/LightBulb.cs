namespace Ducc;

public sealed class LightBulb : Component
{
	[Property] public PointLight Light { get; set; }
	[Property] public ModelRenderer LightModel { get; set; }
	[Property] public SoundPointComponent EnableSound { get; set; }
	[Property] public SoundPointComponent DisableSound { get; set; }
	[Property] public SoundPointComponent FlickerSound { get; set; }
	[Property] public string EnabledMaterialGroup { get; set; }
	[Property] public string DisabledMaterialGroup { get; set; }
	[Property] public Color LightColor { get; set; } = Color.White;
	[Property] public bool LightEnabled => _lightEnabled;
	private bool _lightEnabled = false;
	private RealTimeUntil _untilEndFlicker;

	protected override void OnStart()
	{
		_lightEnabled = Light.IsValid() && Light.LightColor != Color.Black;
	}

	protected override void OnUpdate()
	{
		if ( _untilEndFlicker )
		{
			EndFlicker();
		}
	}

	[Button( "Flicker" )]
	public void Flicker()
	{
		var onColor = LightColor;
		if ( 1 >= Game.Random.Int(1_000_000) )
		{
			onColor = Color.Red;
		}
		Light.LightColor = LightEnabled ? Color.Black : onColor;
		SetMaterialGroup( !LightEnabled );
		_untilEndFlicker = 0.1f;
		if ( FlickerSound.IsValid() )
		{
			FlickerSound.StartSound();
		}
	}

	private void EndFlicker()
	{
		Light.LightColor = _lightEnabled ? LightColor : Color.Black;
		SetMaterialGroup( LightEnabled );
		if ( FlickerSound.IsValid() )
		{
			FlickerSound.StopSound();
		}
	}

	[Button("Turn On")]
	public void TurnOn()
	{
		if ( !Light.IsValid() )
			return;

		Light.LightColor = LightColor;
		if ( !LightEnabled && EnableSound.IsValid() )
		{
			EnableSound.StartSound();
		}
		SetMaterialGroup( false );
		_lightEnabled = true;
	}

	[Button("Turn Off")]
	public void TurnOff()
	{
		if ( !Light.IsValid() )
			return;

		Light.LightColor = Color.Black;
		if ( LightEnabled && DisableSound.IsValid() )
		{
			DisableSound.StartSound();
		}
		SetMaterialGroup( false );
		_lightEnabled = false;
	}

	private void SetMaterialGroup( bool enabled )
	{
		if ( !LightModel.IsValid() )
			return;

		if ( enabled && !string.IsNullOrWhiteSpace( EnabledMaterialGroup ) )
		{
			LightModel.MaterialGroup = EnabledMaterialGroup;
			return;
		}
		if ( !enabled && !string.IsNullOrWhiteSpace( DisabledMaterialGroup ) )
		{
			LightModel.MaterialGroup = DisabledMaterialGroup;
			return;
		}
	}
}
