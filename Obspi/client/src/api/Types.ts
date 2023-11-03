export interface IDigitalIoResponse {
    name: string,
    value: boolean,
}

export interface IDiagnostic {
    name: string,
    value: string,
    unit: string,
}

export interface IObservatoryState {
    isRoofSafeToMove: boolean,
    isRoofOpen: boolean,
    isRoofClosed: boolean,
}

export interface IWeatherSnapshot {
    timestamp: Date,
    cloudCover: number,
    dewPoint: number,
    humidity: number,
    pressure: number,
    skyBrightness: number,
    sqm: number,
    skyQuality: number,
    skyTemperature: number,
    temperature: number,
    windSpeed: number,
}