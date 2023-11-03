import { useState } from 'react';
import { Box, SxProps, Theme, useMediaQuery, useTheme } from '@mui/material';
import SideNav from './components/SideNav';
import AppHeader from './components/AppHeader';
import AppRoutes from './router/AppRoutes';

function App() {
  const theme = useTheme();
  const onlySmallScreen = useMediaQuery(theme.breakpoints.down("sm"));

  const [collapsed, setCollapsed] = useState(true);
  const [toggled, setToggled] = useState(true);
  const [broken, setBroken] = useState(onlySmallScreen);

  const handleToggle = (): void => {
    if (broken) {
      setCollapsed(false);
      setToggled(!toggled);
    } else {
      setCollapsed(!collapsed);
    }
  }

  const handleBrokenChanged = (newBroken: boolean): void => {
    if (broken && !newBroken) {
      setCollapsed(true);
    }
    setBroken(newBroken);
  }

  return (
    <>
      <AppHeader handleToggle={handleToggle} />
      <Box sx={styles.container}>
        <SideNav
          collapsed={collapsed}
          toggled={toggled}
          broken={broken}
          onBreakPoint={handleBrokenChanged}
          setToggled={setToggled} />
        <Box component={'main'} sx={styles.mainSection}>
          <AppRoutes />
        </Box>
      </Box>
    </>
  )
}

const styles: Record<string, SxProps<Theme>> = {
  container: {
    display: 'flex',
    bgcolor: 'neutral.light',
    height: 'calc(100% - 64px)',
  },
  mainSection: {
    padding: 1,
    width: '100%',
    height: '100%',
    overflow: 'auto'
  }
};

export default App
