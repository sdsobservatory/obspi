import { Box, Card, CardMedia, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from "@mui/material"
import {
    GOES_AIRMASS_URL, GOES_BAND1_URL, GOES_BAND2_URL, GOES_BAND3_URL, GOES_BAND4_URL,
    GOES_BAND5_URL, GOES_BAND6_URL, GOES_BAND7_URL, GOES_BAND8_URL, GOES_BAND9_URL,
    GOES_BAND10_URL, GOES_BAND11_URL, GOES_BAND12_URL, GOES_BAND13_URL,
    GOES_BAND14_URL, GOES_BAND15_URL, GOES_BAND16_URL,
    GOES_DAYCLOUDPHASE_URL, GOES_DAYNIGHTCLOUDMICROCOMBO_URL, GOES_DUST_URL,
    GOES_FIRETEMPERATURE_URL, GOES_GEOCOLOR_URL, GOES_NIGHTMICROPHYSICS_URL,
    GOES_SANDWICH_URL } from "../constants"
import React from "react";

enum Band {
    Band1 = "Band 1 (Blue)",
    Band2 = "Band 2 (Red)",
    Band3 = "Band 3 (NIR)",
    Band4 = "Band 4 (Cirrus)",
    Band5 = "Band 5 (Snow/Ice)",
    Band6 = "Band 6 (Cloud Particle)",
    Band7 = "Band 7 (Shortwave)",
    Band8 = "Band 8 (Upper Water)",
    Band9 = "Band 9 (Mid Water)",
    Band10 = "Band 10 (Low Water)",
    Band11 = "Band 11 (Cloud Top)",
    Band12 = "Band 12 (Ozone)",
    Band13 = "Band 13 (Clean Longwave)",
    Band14 = "Band 14 (Longwave)",
    Band15 = "Band 15 (Dirty Longwave)",
    Band16 = "Band 16 (CO2 Longwave)",
    AirMass = "Air Mass",
    DayCloudPhase = "Day Cloud Phase",
    DayNightCloudMicroCombo = "Day Night Cloud Micro Combo",
    Dust = "Dust",
    FireTemperature = "Fire Temperature",
    GeoColor = "Geo Color",
    NightMicroPhysics = "Night Micro Physics",
    Sandwich = "Sandwich",
};

function GoesViewer() {
    const [band, setBand] = React.useState(GOES_BAND11_URL);
    const [key, setKey] = React.useState(new Date().getTime());

    const handleChange = (event: SelectChangeEvent) => {
        setBand(event.target.value as Band);
    };

    const reloadImg = () => {
        // The NOAA url is the same and to break the cache we need to change the url
        setKey(new Date().getTime());
    };

    React.useEffect(() => {
        // GOES image updates every 5 min
        const intervalId = setInterval(reloadImg, 300000);
        return () => clearInterval(intervalId);
    }, [band, key]);

  return (
    <Card sx={{display: "flex"}}>
        <Box sx={{ position: "relative" }}>
            <CardMedia
                component="img"
                sx={{ width: 800 }}
                image={band + "?" + key}
                alt="GOES Band"/>
            <Box 
                sx={{
                    position: "absolute",
                    top: 0,
                    left: 0,
                    width: "100%",
                    bgcolor: "rgba(0, 0, 0, 0.70)",
                    padding: "10px",
                }}>
                <FormControl
                    fullWidth
                    sx={{
                        margin: 0,
                    }}>
                    <InputLabel id="select-band-label">Band</InputLabel>
                    <Select
                        displayEmpty
                        id="select-band"
                        size="small"
                        value={band}
                        label="GOES Band"
                        onChange={handleChange}>
                        <MenuItem value={GOES_BAND1_URL}>{Band.Band1}</MenuItem>
                        <MenuItem value={GOES_BAND2_URL}>{Band.Band2}</MenuItem>
                        <MenuItem value={GOES_BAND3_URL}>{Band.Band3}</MenuItem>
                        <MenuItem value={GOES_BAND4_URL}>{Band.Band4}</MenuItem>
                        <MenuItem value={GOES_BAND5_URL}>{Band.Band5}</MenuItem>
                        <MenuItem value={GOES_BAND6_URL}>{Band.Band6}</MenuItem>
                        <MenuItem value={GOES_BAND7_URL}>{Band.Band7}</MenuItem>
                        <MenuItem value={GOES_BAND8_URL}>{Band.Band8}</MenuItem>
                        <MenuItem value={GOES_BAND9_URL}>{Band.Band9}</MenuItem>
                        <MenuItem value={GOES_BAND10_URL}>{Band.Band10}</MenuItem>
                        <MenuItem value={GOES_BAND11_URL}>{Band.Band11}</MenuItem>
                        <MenuItem value={GOES_BAND12_URL}>{Band.Band12}</MenuItem>
                        <MenuItem value={GOES_BAND13_URL}>{Band.Band13}</MenuItem>
                        <MenuItem value={GOES_BAND14_URL}>{Band.Band14}</MenuItem>
                        <MenuItem value={GOES_BAND15_URL}>{Band.Band15}</MenuItem>
                        <MenuItem value={GOES_BAND16_URL}>{Band.Band16}</MenuItem>
                        <MenuItem value={GOES_AIRMASS_URL}>{Band.AirMass}</MenuItem>
                        <MenuItem value={GOES_DAYCLOUDPHASE_URL}>{Band.DayCloudPhase}</MenuItem>
                        <MenuItem value={GOES_DAYNIGHTCLOUDMICROCOMBO_URL}>{Band.DayNightCloudMicroCombo}</MenuItem>
                        <MenuItem value={GOES_DUST_URL}>{Band.Dust}</MenuItem>
                        <MenuItem value={GOES_FIRETEMPERATURE_URL}>{Band.FireTemperature}</MenuItem>
                        <MenuItem value={GOES_GEOCOLOR_URL}>{Band.GeoColor}</MenuItem>
                        <MenuItem value={GOES_NIGHTMICROPHYSICS_URL}>{Band.NightMicroPhysics}</MenuItem>
                        <MenuItem value={GOES_SANDWICH_URL}>{Band.Sandwich}</MenuItem>
                    </Select>
                </FormControl>
            </Box>
        </Box>
        
        
    </Card>
  );
}

export default GoesViewer