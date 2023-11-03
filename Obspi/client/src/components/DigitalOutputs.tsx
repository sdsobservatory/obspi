import { useQuery, useQueryClient, useMutation } from "@tanstack/react-query";
import { getOutputs, setOutput } from "../api/Api";
import { Card, CardContent, Typography, Stack } from "@mui/material";
import DigitalOutput from "../components/DigitalOutput";

function DigitalOutputs() {
    const {
        isLoading,
        isError,
        error,
        data: outputs,
    } = useQuery(["digitalOutputs"], getOutputs, {
        refetchInterval: 1000,
    });

    const queryClient = useQueryClient();
    const mutation = useMutation({
        mutationFn: setOutput,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["digitalOutputs"] });
        }
    });

    if (isLoading) return "Loading...";
    if (isError) {
        console.error(error);
        return "Error";
    }

    return (
        <Card>
            <CardContent>
                <Typography variant="h4" sx={{ pb: 1 }}>Digital Outputs</Typography>
                <Stack spacing={1}>
                    {outputs.map(output => 
                        <DigitalOutput
                            key={output.name}
                            name={output.name}
                            value={output.value}
                            handleChange={(n, v) => mutation.mutateAsync({name: n, value: v})}
                        />)}
                </Stack>
            </CardContent>
        </Card>
    )
}

export default DigitalOutputs;