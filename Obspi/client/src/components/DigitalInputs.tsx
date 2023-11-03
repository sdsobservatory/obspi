import { useQuery } from "@tanstack/react-query";
import { getInputs } from "../api/Api";
import { Card, CardContent, Typography, Stack } from "@mui/material";
import DigitalIoIndicator from "./DigitalIoIndicator";

function DigitalInputs() {
    const {
        isLoading,
        isError,
        error,
        data: inputs,
    } = useQuery(["digitalInputs"], getInputs, {
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
                <Typography variant="h4" sx={{ pb: 1 }}>Digital Inputs</Typography>
                <Stack spacing={1}>
                    {inputs.map(input => <DigitalIoIndicator key={input.name} label={input.name} value={input.value} />)}
                </Stack>
            </CardContent>
        </Card>
    )
}

export default DigitalInputs;