import { BingMap } from "./BingMap";
import { DevTools } from "./DevTools";

declare global {
    interface Window {
        rpedrettiBlazorComponents: {
            bingMap: {
                map: BingMap,
                devTools: DevTools
            }
        };
        getBingMap: Function
    }
}
