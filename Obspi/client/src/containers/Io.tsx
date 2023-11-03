import { Grid, Alert } from "@mui/material"
import DigitalOutputs from "../components/DigitalOutputs"
import DigitalInputs from "../components/DigitalInputs"
import { Warning } from "@mui/icons-material"

function Io() {
  return (
      <Grid container spacing={2} p={1}>
        <Grid item xs={12}>
          <Alert
            variant="filled"
            severity="warning"
            icon={<Warning />}>
              Toggling outputs is immediate! Be sure you toggle the correct output.
          </Alert>
        </Grid>
          <Grid item sm={6} sx={{ width: "100%" }}>
            <DigitalOutputs />
          </Grid>
          <Grid item sm={6} sx={{ width: "100%" }}>
            <DigitalInputs />
          </Grid>
      </Grid>
  )
}

export default Io