import { Grid, Stack } from "@mui/material"
import GrafanaComponent from "../components/GrafanaComponent"
import CameraComponent from "../components/CameraComponent"
import AllSkyComponent from "../components/AllSkyComponent"
import {
  GRAFANA_ASTRODARKEND_EMBED_URL, GRAFANA_ASTRODARKSTART_EMBED_URL, GRAFANA_HUMIDITY_EMBED_URL,
  GRAFANA_MOON_ILLUMINATION_EMBED_URL, GRAFANA_SAFETY_EMBED_URL, GRAFANA_SQM_EMBED_URL,
  GRAFANA_SUNRISE_EMBED_URL, GRAFANA_SUNSET_EMBED_URL, GRAFANA_TEMPERATURE_EMBED_URL,
  GRAFANA_WIND_EMBED_URL, CAMERA_SOUTH_URL, CAMERA_ALLSKY_URL } from "../constants"

function Dashboard() {
  return (
    <Grid container>
      <Grid item md={6} pb={1}>
        <Stack
          direction="row"
          spacing={1}
          justifyContent="center"
          alignItems="center"
          useFlexGap
          flexWrap="wrap">

          {/* Temperature */}
          <GrafanaComponent url={GRAFANA_TEMPERATURE_EMBED_URL} />

          {/* SQM */}
          <GrafanaComponent url={GRAFANA_SQM_EMBED_URL} />

          {/* Humidity */}
          <GrafanaComponent url={GRAFANA_HUMIDITY_EMBED_URL} />

          {/* Wind */}
          <GrafanaComponent url={GRAFANA_WIND_EMBED_URL} />

          {/* Moon Illumination */}
          <GrafanaComponent url={GRAFANA_MOON_ILLUMINATION_EMBED_URL} />

          {/* Safety */}
          <GrafanaComponent url={GRAFANA_SAFETY_EMBED_URL} />

          {/* Sunset */}
          <GrafanaComponent url={GRAFANA_SUNSET_EMBED_URL} />

          {/* Sunrise */}
          <GrafanaComponent url={GRAFANA_SUNRISE_EMBED_URL} />

          {/* Astro Dark Starts */}
          <GrafanaComponent url={GRAFANA_ASTRODARKSTART_EMBED_URL} />

          {/* Astro Dark Ends */}
          <GrafanaComponent url={GRAFANA_ASTRODARKEND_EMBED_URL} />

        </Stack>
      </Grid>
      <Grid item md={6}>
        <Stack
          spacing={2}
          direction="column"
          alignItems="stretch"
          justifyContent="flex-start">
          <CameraComponent
            hideHeader={true}
            name="South"
            url={CAMERA_SOUTH_URL} />
          <AllSkyComponent
            hideHeader={true}
            aspectRatio={4 / 3}
            name="All Sky"
            url={CAMERA_ALLSKY_URL} />
        </Stack>
      </Grid>
    </Grid>

  )
}

export default Dashboard