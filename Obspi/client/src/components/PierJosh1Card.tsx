import { Card, CardContent, Grid, Typography, Stack } from "@mui/material"
import { restartPierAc, restartPierDc } from "../api/Api";
import CommandButton from "./CommandButton";

function PierJosh1Card() {
  return (
    <Card>
      <CardContent>
        <Typography gutterBottom variant="h5">Josh CDK Pier</Typography>
        <Grid container>

          <Grid item>
            <Stack direction="row" spacing={1}>
              <CommandButton buttonText="Restart AC" handleClick={() => restartPierAc("josh1")} />
              <CommandButton buttonText="Restart DC" handleClick={() => restartPierDc("josh1")} />
            </Stack>
          </Grid>

        </Grid>
      </CardContent>
    </Card>
  )
}

export default PierJosh1Card;