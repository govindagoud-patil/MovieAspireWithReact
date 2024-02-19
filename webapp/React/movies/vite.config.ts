import react from '@vitejs/plugin-react-swc'


import { ConfigEnv, defineConfig, loadEnv } from 'vite';

export default defineConfig(({command, mode} : ConfigEnv) => {
    console.log(`configuring vite with command: ${command}, mode: ${mode}`);
    // suppress eslint warning that process isn't defined (it is)
    // eslint-disable-next-line
    const cwd = process.cwd();
    console.log(`loading envs from ${cwd} ...`);
    const env = {...loadEnv(mode, cwd, 'VITE_')};
    console.log(`loaded env: ${JSON.stringify(env)}`);

    // reusable config for both server and preview
    const serverConfig = {
        host: true,
        port: Number(env.VITE_APP_PORT),
        strictPort: true,
    };

    return {
        base: '/',
        plugins: [react()],
        preview: serverConfig,
        server: serverConfig,
    };
});