import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    thresholds: {
        "http_reqs{status:200}": ["count>1"],
        "http_reqs{status:404}": ["count>1"],
    },
};

export default function () {
    http.get(`https://${__ENV.HOSTNAME}/beta`);
    sleep(1);
}
