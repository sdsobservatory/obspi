import { Card, CardContent, Grid, Stack, Typography } from "@mui/material"
import { useQuery } from "@tanstack/react-query";
import { getObservatoryState, openRoof, closeRoof, stopRoof } from "../api/Api";
import CommandButton from "./CommandButton";
import DigitalIoIndicator from "./DigitalIoIndicator";

function ObservatoryCard() {
    const {
        isLoading,
        isError,
        error,
        data: observatoryState,
    } = useQuery(["observatoryState"], getObservatoryState, {
        refetchInterval: 1000,
    });

    if (isLoading) return "Loading...";
    if (isError) {
        console.error(error);
        return "Error";
    }

    return (
        <Card>
            <CardContent>
                <Typography gutterBottom variant="h5">Roof</Typography>
                <Grid container>

                    <Grid item md={4} sx={{ width: "100%" }}>
                        <DigitalIoIndicator label="Safe To Move" value={observatoryState.isRoofSafeToMove} />
                    </Grid>

                    <Grid item md={4} sx={{ width: "100%" }}>
                        <DigitalIoIndicator label="Is Open" value={observatoryState.isRoofOpen} />
                    </Grid>

                    <Grid item md={4} sx={{ width: "100%" }}>
                        <DigitalIoIndicator label="Is Closed" value={observatoryState.isRoofClosed} />
                    </Grid>

                    <Grid item md={3} mt={2} sx={{ width: "100%" }}>
                        <Stack direction="row" spacing={1}>
                            <CommandButton buttonText="Open" handleClick={() => openRoof()} />
                            <CommandButton buttonText="Close" handleClick={() => closeRoof()} />
                            <CommandButton buttonText="Stop" color="error" handleClick={() => stopRoof()} />
                        </Stack>
                    </Grid>

                </Grid>
            </CardContent>
        </Card>
    )
}

export default ObservatoryCard;