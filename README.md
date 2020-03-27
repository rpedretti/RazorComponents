# RazorComponents

This project aims to provide reusable components for Blazor application.

Live samples at

**TBD**

## Packages

- BingMaps: https://www.nuget.org/packages/RPedretti.RazorComponents.BingMap/
- I18n: https://www.nuget.org/packages/RPedretti.RazorComponents.I18n/
- Inputs: https://www.nuget.org/packages/RPedretti.RazorComponents.Input/
- Layouts: https://www.nuget.org/packages/RPedretti.RazorComponents.Layout/
- Sensors: https://www.nuget.org/packages/RPedretti.RazorComponents.Sensors/

## I18n
**TBD**

## Inputs
There are four custom inputs, with acessibility

### Usage

- Checkbox: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Inputs/InputsPage.razor#L29-L48)
- RadioGroup: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Inputs/InputsPage.razor#L84-L101)
- SuggestBox: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Inputs/InputsPage.razor#L12-L27)
- ToggleSwitch: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Inputs/InputsPage.razor#L50-L82)
- ProgressBar: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Loaders/LoadersPage.razor#L15)
- Spinner: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Loaders/LoadersPage.razor#L18-L33)

## Layout

### Usage

- Accordeon: There are many in [sample Input](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Inputs/InputsPage.razor) page
- DynamicGroupedTable: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Forecast/ForecastPage.razor#L41-L50)
- DynamicTable: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Forecast/ForecastPage.razor#L54-L65)
- PagedGrid: [sample](https://github.com/rpedretti/RazorComponents/blob/master/Samples/RPedretti.RazorComponents.Sample.Shared/Pages/Movies/MoviesPage.razor#L29-L52)

## BingMaps

### Usage

Load the maps script in your app at the index.html file.
```
    <script src="_content/RPedretti.RazorComponents.BingMap/js/vendors~bing-map_v1.js"></script>
    <script src="_content/RPedretti.RazorComponents.BingMap/js/bing-map_v1.js"></script>
    <script src="_content/RPedretti.RazorComponents.BingMap/js/bing-map-devtool.js"></script>
```

Add the map to your page

```
@page "/"
@using RPedretti.RazorComponents.BingMap
@using RPedretti.RazorComponents.Shared.Components

@inherits BaseComponent

<div class="directions-page">
    <div class="directions-container" onclick="event.stopPropagation();">
        <div class="inputPannel" id="inputPannel"></div>
        <div class="itineraryPanel" id="itineraryPanel"></div>
    </div>
    <BingMap Id="@BingMapId"
             ApiKey="<your_key"
             MapsConfig="@MapsConfig"
             ViewConfig="@MapsViewConfig"
             MapLoaded="@MapLoaded"
             Modules="@Modules" />
</div>
```

To acquire one key follow the instructions [here](https://msdn.microsoft.com/en-us/library/ff428642.aspx)

The component supports dynamic modue loading. The [sample](https://github.com/rpedretti/RazorComponents/tree/master/Samples/RPedretti.RazorComponents.Wasm.BingMap/Pages/Directions)
cover its features.

## Sensors
### Ambient Light Sensor (experimental)
api docs: https://developer.mozilla.org/en-US/docs/Web/API/AmbientLightSensor.

Only available in Chrome: must enable [chrome://flags/#enable-generic-sensor](chrome://flags/#enable-generic-sensor) and [chrome://flags/#enable-generic-sensor-extra-classes](chrome://flags/#enable-generic-sensor-extra-classes)

To use this sensor just call the `AddAmbientLightSensor()` at the `Startup.ConfigureServices` and
`InitAmbientLightSensor()` at `Startup.Configure`

```
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddAmbientLightSensor();
    ...
}

public void Configure(IBlazorApplicationBuilder app)
{
    ...
    app.InitAmbientLightSensor();
    ...
}
```

Then the service will be registered and can be injected anywhere in the application with the `AmbientLightSensor` class.
To get reading subscribe to the event `AmbientLightSensor.OnReading` and for error `AmbientLightSensor.OnError`

### Geolocation

To use this sensor just call the `AddGeolocationSensor()` at the `Startup.ConfigureServices`:

```
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddGeolocationSensor();
    ...
}
```

Then the service will be registered and can be injected anywhere in the application with the `GeolocationSensor` class.
To watch position change subscribe to `OnPositionUpdate` and for error `OnPositionError`

## Network

### SignalR
**TBD**

[sample](https://github.com/rpedretti/BlazorComponents/blob/master/Samples/RPedretti.Blazor.Components.Sample/Pages/SignalR/SignalR.cshtml.cs)
