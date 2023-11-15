import { Grid, Alert, AlertTitle } from "@mui/material"
import DigitalOutputs from "../components/DigitalOutputs"
import DigitalInputs from "../components/DigitalInputs"
import { Warning } from "@mui/icons-material"

function Io() {
  return (
      <Grid container spacing={2} p={1}>
        <Grid item xs={12}>
          <Alert
            variant="outlined"
            severity="warning"
            icon={<Warning />}>
              <AlertTitle>Toggling Outputs Is Immediate</AlertTitle>
              Use caution when toggling outputs. Be sure you are toggling the indended output.
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