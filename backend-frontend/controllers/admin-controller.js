// server/routes/adminRoutes.js
const express = require('express');
const router = express.Router();
const Product = require('../models/Product'); // Import your Product model

// Admin fetches unapproved products
router.get('/unapproved-products', async (req, res) => {
  try {
    // Fetch unapproved products
    const unapprovedProducts = await Product.find({ approval:false });

    res.status(200).json(unapprovedProducts);
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Internal Server Error' });
  }
});

// Admin approves a product
router.post('/approve-product/:productId', async (req, res) => {
  const productId = req.params.productId;

  try {
    // Fetch the product from the database
    const product = await Product.findById(productId);

    if (!product) {
      return res.status(404).json({ message: 'Product not found' });
    }

    // Update the product status to 'approved' and save it
    product.approval = true;
    await product.save();

    res.status(200).json({ message: 'Product approved successfully' });
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Internal Server Error' });
  }
});

module.exports = router;
