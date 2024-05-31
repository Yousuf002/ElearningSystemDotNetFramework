// server/models/Product.js
const mongoose = require('mongoose');

const productSchema = new mongoose.Schema({
  images: [String],       // Array of image URLs
  name: { type: String, required: true },
  description: { type: String, required: true },
  price: { type: Number, required: true },
  prod_code: { type: String, required: true, unique: true },
  seller_id: { type: mongoose.Schema.Types.ObjectId, ref: 'Seller', required: true },
  qty: { type: Number, default: 0 },
  approval: { type: Boolean, enum: ['unapproved', 'approved'], default: 'unapproved' },
  discount: { type: Number, default: 0 },
});

const Product = mongoose.model('Product', productSchema);

module.exports = Product;
