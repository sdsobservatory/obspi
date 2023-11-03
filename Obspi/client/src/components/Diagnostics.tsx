import { Card, CardContent, Typography, Stack } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import { getDiagnostics } from "../api/Api";
import Diagnostic from "./Diagnostic";

function Diagnostics() {
    const {
        isLoading,
        isError,
        error,
        data: diagnostics,
    } = useQuery(["diagnostics"], getDiagnostics, {
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
                <Typography variant="h4" sx={{ pb: 1 }}>Diagnostics</Typography>
                <Stack spacing={1}>
                    {diagnostics.map(item => <Diagnostic diagnostic={item} key={item.name} />)}
                </Stack>
            </CardContent>
        </Card>
    )
}

export default Diagnostics;