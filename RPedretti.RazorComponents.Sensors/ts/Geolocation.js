"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Geolocation {
    constructor() {
        this.watchPosition = (objectRef) => {
            if (navigator.geolocation) {
                const id = navigator.geolocation.watchPosition(p => objectRef.invokeMethodAsync('WatchPositionResponse', this.mapPosition(p)), e => objectRef.invokeMethodAsync('WatchPositionError', this.mapPositionError(e)));
                return id;
            }
            else {
                const error = { code: -1, message: "Geolocation not available" };
                objectRef.invokeMethodAsync('WatchPositionError', error);
                null;
            }
        };
        this.stopWatchPosition = (watchId) => {
            if (navigator.geolocation) {
                navigator.geolocation.clearWatch(watchId);
            }
        };
    }
    mapPosition(position) {
        return {
            coords: position.coords,
            timestamp: new Date(position.timestamp)
        };
    }
    ;
    mapPositionError(positionError) {
        return {
            code: positionError.code,
            message: positionError.message
        };
    }
    ;
}
exports.Geolocation = Geolocation;
;
//# sourceMappingURL=Geolocation.js.map