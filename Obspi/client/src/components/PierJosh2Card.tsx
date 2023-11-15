import { Card, CardContent, Grid, Typography, Stack } from "@mui/material"
import { restart10Micron, restartPierAc, restartPierDc } from "../api/Api";
import CommandButton from "./CommandButton";

function PierJosh2Card() {
  return (
    <Card>
      <CardContent>
        <Typography gutterBottom variant="h5">Josh SVX Pier</Typography>
        <Grid container>

          <Grid item>
            <Stack direction="row" spacing={1}>
              <CommandButton buttonText="Restart AC" handleClick={() => restartPierAc("josh2")} />
              <CommandButton buttonText="Restart DC" handleClick={() => restartPierDc("josh2")} />
              <CommandButton buttonText="Toggle Mount" handleClick={() => restart10Micron("josh")} />
            </Stack>
          </Grid>

        </Grid>
      </CardContent>
    </Card>
  )
}

export default PierJosh2Card;