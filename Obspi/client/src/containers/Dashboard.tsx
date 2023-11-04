import { Grid, Stack, Box, Card, CardMedia } from "@mui/material"
import GrafanaComponent from "../components/GrafanaComponent"
import CameraComponent from "../components/CameraComponent"
import AllSkyComponent from "../components/AllSkyComponent"
import {
  GRAFANA_ASTRODARKEND_EMBED_URL, GRAFANA_ASTRODARKSTART_EMBED_URL, GRAFANA_HUMIDITY_EMBED_URL,
  GRAFANA_MOON_ILLUMINATION_EMBED_URL, GRAFANA_SAFETY_EMBED_URL, GRAFANA_SQM_EMBED_URL,
  GRAFANA_SUNRISE_EMBED_URL, GRAFANA_SUNSET_EMBED_URL, GRAFANA_TEMPERATURE_EMBED_URL,
  GRAFANA_WIND_EMBED_URL, CAMERA_SOUTH_URL, CAMERA_ALLSKY_URL, GRAFANA_MEDIAN_GUIDE_ERROR_EMBED_URL } from "../constants"

function Dashboard() {
  return (
    <Grid container>
      <Grid item md={6} pb={1}>
        <Stack
          direction="column">

          <Stack
            direction="row"
            spacing={2}
            padding={1}
            justifyContent="space-evenly"
            alignItems="center"
            useFlexGap
            flexWrap="wrap">

            <GrafanaComponent url={GRAFANA_TEMPERATURE_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_SQM_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_HUMIDITY_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_WIND_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_MOON_ILLUMINATION_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_SAFETY_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_SUNSET_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_SUNRISE_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_ASTRODARKSTART_EMBED_URL} />
            <GrafanaComponent url={GRAFANA_ASTRODARKEND_EMBED_URL} />

          </Stack>

          <Box padding={1}>
            <Card>
              <CardMedia
                  src={GRAFANA_MEDIAN_GUIDE_ERROR_EMBED_URL}
                  sx={{
                      border: "0px",
                      pointerEvents: "none",
                      height: "400px"
                  }}
                  component="iframe"
              />
            </Card>
          </Box>

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