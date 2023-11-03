import { Card, CardContent, Grid, Typography, Stack } from "@mui/material"
import { restart10Micron, restartPierAc, restartPierDc } from "../api/Api";
import CommandButton from "./CommandButton";

function PierAlexCard() {
  return (
    <Card>
      <CardContent>
        <Typography gutterBottom variant="h5">Alex Pier</Typography>
        <Grid container>

          <Grid item>
            <Stack direction="row" spacing={1}>
              <CommandButton buttonText="Restart AC" handleClick={() => restartPierAc("alex")} />
              <CommandButton buttonText="Restart DC" handleClick={() => restartPierDc("alex")} />
              <CommandButton buttonText="Restart Mount" handleClick={() => restart10Micron("alex")} />
            </Stack>
          </Grid>

        </Grid>
      </CardContent>
    </Card>
  )
}

export default PierAlexCard;