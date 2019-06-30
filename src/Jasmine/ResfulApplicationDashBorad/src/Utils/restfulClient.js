import axios from 'axios'
import iview from 'iview'

const client = axios.create({
})

client.interceptors.request.use((config) => {
    iview.LoadingBar.start();
    return config;
}, (_err) => {
    iview.LoadingBar.error();
    return _err;
})

client.interceptors.response.use(response => {
    iview.LoadingBar.finish();
    return response;
}, _err => {
        iview.LoadingBar.error();
        return _err;
    })

export default client
