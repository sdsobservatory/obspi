import { Grid, } from "@mui/material"
import ObservatoryCard from "../components/ObservatoryCard"
import PierCharlieCard from "../components/PierCharlieCard"
import PierJosh2Card from "../components/PierJosh2Card"
import PierAlexCard from "../components/PierAlexCard"
import PierJosh1Card from "../components/PierJosh1Card"

function Observatory() {
  return (
    <Grid container p={1} spacing={1}>

      <Grid item md={12} sx={{ width: "100%" }}>
        <ObservatoryCard />
      </Grid>

      <Grid item md={6} sx={{ width: "100%" }}>
        <PierCharlieCard />
      </Grid>

      <Grid item md={6} sx={{ width: "100%" }}>
        <PierJosh2Card />
      </Grid>

      <Grid item md={6} sx={{ width: "100%" }}>
        <PierAlexCard />
      </Grid>

      <Grid item md={6} sx={{ width: "100%" }}>
        <PierJosh1Card />
      </Grid>

    </Grid>
  )
}

export default Observatory