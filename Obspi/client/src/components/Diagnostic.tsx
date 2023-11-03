import { IDiagnostic } from "../api/Types"
import { Grid, Typography } from "@mui/material"

interface DiagnosticProps {
  diagnostic: IDiagnostic
}

function Diagnostic(props: DiagnosticProps) {
  return (
    <Grid container spacing={1} key={props.diagnostic.name}>
      <Grid item xs={4}>
        <Typography>{props.diagnostic.name}</Typography>
      </Grid>
      <Grid item xs={7}>
        <Typography noWrap>{props.diagnostic.value}</Typography>
      </Grid>
      <Grid item xs={1}>
        <Typography>{props.diagnostic.unit}</Typography>
      </Grid>
    </Grid>
  )
}

export default Diagnostic;