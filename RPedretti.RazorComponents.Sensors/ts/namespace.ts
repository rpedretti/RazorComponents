import { Geolocation } from "./Geolocation";
import { LightSensor } from "./LightSensor";

declare global {
    interface Window {
        rpedrettiBlazorSensors: {
            geolocation: Geolocation,
            lightSensor: LightSensor
        }
    }
}
