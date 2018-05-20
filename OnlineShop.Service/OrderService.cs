using System;
using System.Collections.Generic;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> orderDetails);
    }
    public class OrderService:IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderId = order.Id;
                    _orderDetailRepository.Add(orderDetail);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
