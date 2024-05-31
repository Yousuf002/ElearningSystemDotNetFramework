const express = require('express');
const dotenv = require('dotenv');
import adminRoutes from "./controllers/admin-controller";
dotenv.config();

// Log loaded environment variables
console.log(process.env);

const app = express();
app.use(express.json());

app.get('/', (req, res) => {
    res.status(200).send({
        message: "server running",
    });
});
app.use('/api/admin',adminRoutes );
const port = process.env.PORT || 5000;

app.listen(port, () => {
    console.log(`Server running in ${process.env.NODE_ENV} Mode on port ${process.env.PORT}`);
});
