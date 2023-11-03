import { IDiagnostic, IDigitalIoResponse, IObservatoryState, IWeatherSnapshot } from "./Types";
import apiClient from "../http-common";

export const getOutputs = async (): Promise<IDigitalIoResponse[]> => {
    const response = await apiClient.get<IDigitalIoResponse[]>("/io/outputs");
    return response.data;
};

export const getInputs = async (): Promise<IDigitalIoResponse[]> => {
    const response = await apiClient.get<IDigitalIoResponse[]>("/io/inputs");
    return response.data;
};

export const setOutput = async (data: IDigitalIoResponse): Promise<void> => {
    await apiClient
        .post(
            `/io/outputs/${data.name}`,
            null,
            {
                params: {
                    state: data.value,
                }
            }
        );
};

export const getDiagnostics = async (): Promise<IDiagnostic[]> => {
    const response = await apiClient.get<IDiagnostic[]>("/diagnostics");
    return response.data;
};

export const getObservatoryState = async (): Promise<IObservatoryState> => {
    const response = await apiClient.get<IObservatoryState>("/observatory");
    return response.data;
};

export const getWeatherSnapshot = async (): Promise<IWeatherSnapshot> => {
    const response = await apiClient.get<IWeatherSnapshot>("/weather");
    return response.data;
};

export const restartPierAc = async (id: string): Promise<void> => {
    console.log("sending restart pier command");
    await apiClient.post(
        `/observatory/command/restart_${id}_ac`,
        null,
        {
            timeout: 10000,
        });
};

export const restartPierDc = async (id: string): Promise<void> => {
    await apiClient.post(
        `/observatory/command/restart_${id}_dc`,
        null,
        {
            timeout: 10000,
        });
};

export const restart10Micron = async (id: string): Promise<void> => {
    await apiClient.post(
        `/observatory/command/restart_${id}_10micron`,
        null,
        {
            timeout: 10000,
        });
};

export const openRoof = async (): Promise<void> => {
    await apiClient.post(
        "/observatory/command/open_roof",
        null,
        {
            timeout: 120000,
        });
};

export const closeRoof = async (): Promise<void> => {
    await apiClient.post(
        "/observatory/command/close_roof",
        null,
        {
            timeout: 120000,
        });
};

export const stopRoof = async (): Promise<void> => {
    await apiClient.post(
        "/observatory/command/stop_roof",
        null,
        {
            timeout: 3000,
        });
};
