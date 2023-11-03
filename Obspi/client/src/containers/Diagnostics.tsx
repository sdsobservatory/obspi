import { Grid } from "@mui/material";
import { default as Diags } from "../components/Diagnostics";

function Diagnostics() {
  return (
    <Grid container p={1}>
      <Grid item lg={6} sx={{ width: "100%" }}>
          <Diags />
      </Grid>
    </Grid>
  )
}

export default Diagnostics