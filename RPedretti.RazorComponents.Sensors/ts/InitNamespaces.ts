import { LightSensor } from "./LightSensor";
import { Geolocation } from "./Geolocation";

namespace rpedrettiBlazorSensors {
    export const lightSensor = new LightSensor();
    export const geolocation = new Geolocation();
}

declare global {
    interface Window {
        rpedrettiBlazorSensors: typeof rpedrettiBlazorSensors;
    }
}

window.rpedrettiBlazorSensors = rpedrettiBlazorSensors;
