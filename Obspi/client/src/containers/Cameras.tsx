import { Grid } from "@mui/material";
import CameraComponent from "../components/CameraComponent"
import AllSkyComponent from "../components/AllSkyComponent"
import { CAMERA_ALLSKY_URL, CAMERA_NORTH_URL, CAMERA_SOUTH_URL, CAMERA_WEST_URL } from "../constants";

function Cameras() {
  return (
    <Grid container spacing={2}>
      <Grid item md={6}>
        <CameraComponent name="North" url={CAMERA_NORTH_URL} />
      </Grid>
      <Grid item md={6}>
        <CameraComponent name="West" url={CAMERA_WEST_URL} />
      </Grid>
      <Grid item md={6}>
        <CameraComponent name="South" url={CAMERA_SOUTH_URL} />
      </Grid>
      <Grid item md={6}>
        <AllSkyComponent name="All Sky" url={CAMERA_ALLSKY_URL} />
      </Grid>
    </Grid>
  )
}

export default Cameras;