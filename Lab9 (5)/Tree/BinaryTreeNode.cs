using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9__5_.Tree
{
    class BinaryTreeNode<T>
    {
        public T data;
        public BinaryTreeNode<T> left = null;
        public BinaryTreeNode<T> right = null;
    }

    class BinaryTree<T>
    {
        BinaryTreeNode<T> root = null;

        public BinaryTreeNode<T> AddRoot(T elem)
        {
            root = new BinaryTreeNode<T> { data = elem };
            return root;
        }

        public BinaryTreeNode<T> AddLeft(BinaryTreeNode<T> node, T elem)
        {
            var newNode = new BinaryTreeNode<T> { data = elem };
            node.left = newNode;
            return newNode;
        }

        public BinaryTreeNode<T> AddRight(BinaryTreeNode<T> node, T elem)
        {
            var newNode = new BinaryTreeNode<T> { data = elem };
            node.right = newNode;
            return newNode;
        }
        

    }
}
