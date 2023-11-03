import { Card, CardContent, Grid, Typography, Stack } from "@mui/material"
import { restartPierAc, restartPierDc } from "../api/Api";
import CommandButton from "./CommandButton";

function PierCharlieCard() {
  return (
    <Card>
      <CardContent>
        <Typography gutterBottom variant="h5">Charlie Pier</Typography>
        <Grid container>

          <Grid item>
            <Stack direction="row" spacing={1}>
              <CommandButton buttonText="Restart AC" handleClick={() => restartPierAc("charlie")} />
              <CommandButton buttonText="Restart DC" handleClick={() => restartPierDc("charlie")} />
            </Stack>
          </Grid>

        </Grid>
      </CardContent>
    </Card>
  )
}

export default PierCharlieCard;